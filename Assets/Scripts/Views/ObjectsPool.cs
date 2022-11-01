using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public static ObjectsPool Instance;

    private List<GameObject> _poolObjects = new List<GameObject>();
    [SerializeField] private int _amountPool = 30;
    [SerializeField] private GameObject _spawnObjct;

    private bool _isFull = false;

    private void Awake()
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
        tmpObj.SetActive(true);
        _poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
