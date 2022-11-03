using System.Collections.Generic;
using Models;
using UnityEngine;

public class ObjectsPoolView : MonoBehaviour
{
    public static ObjectsPoolView Instance;
    public delegate UnitView GetPooledObjectEvent(IUnit cubeController);
    public GetPooledObjectEvent getPooledObjectEvent;
    
    public delegate void TurnOfObjectEvent(IUnit unit);
    public TurnOfObjectEvent turnOfObjectEvent;

    private List<UnitView> _poolObjects = new List<UnitView>();
    [SerializeField] private int _amountPool = 128;
    [SerializeField] private GameObject _spawnObjct;

    private bool _isFull = false;

    public void Init(GameModel gameModel)
    {
        Instance = this;
        _amountPool = gameModel.AmountPool;

        for (int i = 0; i < _amountPool; i++)
        {
            GameObject tmpObj = Instantiate(_spawnObjct);
            tmpObj.SetActive(false);
            _poolObjects.Add(tmpObj.GetComponent<UnitView>());
        }

        getPooledObjectEvent = GetPooledObject;
        turnOfObjectEvent = TurnOfObject;
    }


    public UnitView GetPooledObject(IUnit unit)
    {
        for (int i = 0; i < _amountPool; i++)
        {
            if (!_poolObjects[i].gameObject.activeInHierarchy)
            {
                _poolObjects[i].gameObject.SetActive(true);
                _poolObjects[i].Unit = unit;
                
                return _poolObjects[i];
            }

            _isFull = true;
        }

        if (_isFull)
        {
            return CreateNewObject(unit);
        }

        return null;
    }

    public void TurnOfObject(IUnit unit)
    {
       
        for (int i = 0; i < _amountPool; i++)
        {
            if (unit == _poolObjects[i].Unit)
            {
                _poolObjects[i].gameObject.SetActive(false);
            }

        }
    }

    private UnitView CreateNewObject(IUnit unit)
    {
        GameObject tmpObj = Instantiate(_spawnObjct);
        tmpObj.gameObject.SetActive(true);
        
        _amountPool++;
        
        tmpObj.GetComponent<UnitView>().Unit = unit;
        _poolObjects.Add(tmpObj.GetComponent<UnitView>());
        
        return tmpObj.GetComponent<UnitView>();
    }
}
