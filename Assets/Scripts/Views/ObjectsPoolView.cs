using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolView : MonoBehaviour
{
    public static ObjectsPoolView Instance;

    private List<GameObject> _poolObjects = new List<GameObject>();
    [SerializeField] private int _amountPool = 30;
    [SerializeField] private GameObject _spawnObjct;

    private bool _isFull = false;

    public void Init()
    {
        Instance = this;

        for (int i = 0; i < _amountPool; i++)
        {
            GameObject tmpObj = Instantiate(_spawnObjct);
            tmpObj.SetActive(false);
            _poolObjects.Add(tmpObj);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountPool; i++)
        {
            if (!_poolObjects[i].activeInHierarchy)
            {
                _poolObjects[i].SetActive(true);
                return _poolObjects[i];
            }

            _isFull = true;
        }

        if (_isFull)
        {
            return CreateNewObject();
        }

        return null;
    }

    public void TurnOfObject(GameObject obj)
    {
        for (int i = 0; i < _amountPool; i++)
        {
            if (obj == _poolObjects[i])
            {
                _poolObjects[i].SetActive(false);
            }

        }
    }

    private GameObject CreateNewObject()
    {
        GameObject tmpObj = Instantiate(_spawnObjct);
        _amountPool++;
        tmpObj.SetActive(true);
        _poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
