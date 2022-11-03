using System;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    private IUnit unit;
    public Action<Vector3> MoveEvent;

    public IUnit Unit
    {
        set
        {
            unit = value;
            unit.UnitView = this;
            MoveEvent = Move;
        }
        get => unit;
    }
    

    public void Move(Vector3 targetPosition)
    {
        transform.position = targetPosition;
        
    }
}
