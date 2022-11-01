using System;
using UnityEngine;

public abstract class IUnit
{
    public float SpeedMoving;
    public Vector3 TargetPosition;
    private Vector3 _currentPosition = Vector3.zero;

    public virtual bool Move()
    {
        _currentPosition = Vector3.MoveTowards(_currentPosition, TargetPosition, SpeedMoving);
        
        if (Vector3.Distance(_currentPosition,TargetPosition) < 0.01f)
        {
            _currentPosition = TargetPosition;
            return false;
        }
        
        Debug.Log("CURRENT POS");
        Debug.Log(_currentPosition);
        return true;
    }

    public virtual void ResetCurrentPosition()
    {
        _currentPosition = Vector3.zero;
    }
}
