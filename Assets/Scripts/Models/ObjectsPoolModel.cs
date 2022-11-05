using System.Collections.Generic;
using Models;
using UnityEngine;
using Views;

public class ObjectsPoolModel
{
    public static ObjectsPoolModel Instance;
    public int GetPoolCount => _poolObjects.Count;

    private List<PoolList> _poolObjects = new List<PoolList>();
    private int _amountPool = 128;

    private bool _isFull = false;
    private ObjectsPoolView _objectsPoolView;

    public void Init(GameModel gameModel)
    {
        Instance = this;
        _amountPool = gameModel.AmountPool;
        _objectsPoolView = ViewManager.Instance._objectsPoolView;

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

                _objectsPoolView.getPooledObjectEvent?.Invoke(unit._unit);
                return unit._unit;
            }
            _isFull = true;
        }

        if (_isFull)
        {
            IUnit newUnit = CreateNewObject();
            _objectsPoolView.createNewObjectEvent?.Invoke(newUnit);
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
                _objectsPoolView.turnOfObjectEvent?.Invoke(unit._unit);
                unit._unit.ResetCurrentPosition();
                unit._isFree = true;
            }
        }
    }

    private IUnit CreateNewObject()
    {
        Debug.Log("New Object");
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
