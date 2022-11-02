using UnityEngine;

public abstract class IUnit
{
    public float SpeedMoving;
    public Vector3 TargetPosition;
    public Vector3 CurrentPosition = Vector3.forward;

    public UnitView UnitView;

    public virtual bool Move()
    {
        if (UnitView != null)
        {
            UnitView.MoveEvent(CurrentPosition, TargetPosition, SpeedMoving);
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving);
        
            if (Vector3.Distance(CurrentPosition,TargetPosition) < 0.01f)
            {
                CurrentPosition = TargetPosition;
                return false;
            }
        }
        return true;
    }

    public virtual void ResetCurrentPosition()
    {
        CurrentPosition = Vector3.forward;
    }
}
