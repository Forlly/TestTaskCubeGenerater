using UnityEngine;

public abstract class IUnit
{
    public float speedMoving;

    public virtual bool Move(Vector3 position, float speed)
    {
        
        return true;
    }
    
}
