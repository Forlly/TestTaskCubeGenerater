using System;
using System.Collections.Generic;
using UnityEngine;
using Views;

public class ObjectsPoolModel
{
    public static ObjectsPoolModel Instance;

    private List<PoolList> _poolObjects = new List<PoolList>();
    private int _amountPool = 128;

    private bool _isFull = false;
    private ObjectsPoolView _objectsPoolView;

    public void Init()
    {
        Instance = this;
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
                unit._unit.ResetCurrentPosition();
                unit._isFree = true;
                Debug.Log(  unit._isFree );
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
