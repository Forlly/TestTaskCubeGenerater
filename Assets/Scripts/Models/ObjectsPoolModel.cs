using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolModel
{
    public static ObjectsPoolModel Instance;

    private List<PoolList> _poolObjects = new List<PoolList>();
    private int _amountPool = 30;

    private bool _isFull = false;

    public void Init()
    {
        Instance = this;

        for (int i = 0; i < _amountPool; i++)
        {
            PoolList tmpUnit = new PoolList();
            _poolObjects.Add(tmpUnit);
            Debug.Log(_poolObjects[i]._unit);
        }
    }


    public IUnit GetPooledObject()
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._isFree)
            {
                return unit._unit;
            }
            _isFull = true;
        }

        if (_isFull)
        {
            return CreateNewObject();
        }

        return null;
    }

    public void TurnOfObject(IUnit _unit)
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._unit == _unit)
            {
                unit._isFree = false;
            }
            _isFull = true;
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
