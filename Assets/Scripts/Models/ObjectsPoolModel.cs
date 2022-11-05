using System.Collections.Generic;
using Models;
using UnityEngine;
using Views;

public class ObjectsPoolModel
{
    public static ObjectsPoolModel Instance;
    public delegate UnitView GetPooledObjectEvent(IUnit cubeController);
    public GetPooledObjectEvent getPooledObjectEvent;
    
    public delegate void TurnOfObjectEvent(IUnit unit);
    public TurnOfObjectEvent turnOfObjectEvent;
    
    public delegate UnitView CreateNewObjectEvent(IUnit unit);
    public CreateNewObjectEvent createNewObjectEvent;

    private List<PoolList> _poolObjects = new List<PoolList>();
    private int _amountPool = 128;

    private bool _isFull = false;

    public void Init(GameModel gameModel)
    {
        Instance = this;
        _amountPool = gameModel.AmountPool;

        for (int i = 0; i < _amountPool; i++)
        {
            PoolList tmpUnit = new PoolList();
            _poolObjects.Add(tmpUnit);
        }
    }

    public IUnit GetPooledObject()
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._isFree)
            {
                unit._isFree = false;

                getPooledObjectEvent?.Invoke(unit._unit);
                return unit._unit;
            }
            _isFull = true;
        }

        if (_isFull)
        {
            IUnit newUnit = CreateNewObject();
            createNewObjectEvent?.Invoke(newUnit);
            return newUnit;
        }

        return null;
    }

    public void TurnOfObject(IUnit _unit)
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._unit == _unit)
            {
                turnOfObjectEvent?.Invoke(unit._unit);
                unit._unit.ResetCurrentPosition();
                unit._isFree = true;
            }
        }
    }

    private IUnit CreateNewObject()
    {

        PoolList tmpUnit = new PoolList();
        _amountPool++;
        _poolObjects.Add(tmpUnit);
        return tmpUnit._unit;
    }
}

public class PoolList
{
    public bool _isFree = true;
    public IUnit _unit = new CubeController();
}
