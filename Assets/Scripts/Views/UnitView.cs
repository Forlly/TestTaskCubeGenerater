using System;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    private IUnit unit;
    public Action<Vector3, Vector3, float> MoveEvent;

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
    

    public void Move(Vector3 currentPosition, Vector3 targetPosition, float speedMoving)
    {
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speedMoving);
        
        if (Vector3.Distance(currentPosition,targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
        }
    }
}
