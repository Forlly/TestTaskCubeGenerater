using UnityEngine;

public class UnitView : MonoBehaviour
{
    private IUnit unit;

    public IUnit Unit
    {
        set { unit = value; }
        get => unit;
    }
    

    public bool Move(Vector3 currentPosition, Vector3 targetPosition, float speedMoving)
    {
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speedMoving);
        
        if (Vector3.Distance(currentPosition,targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            return false;
        }
        
        return true;
    }
}
