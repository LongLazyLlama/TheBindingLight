using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    Vector3 _origin;

    public Vector3 _destination;
    public Vector3 Displacement;

    public float DESTMoveSpeed = 1; //Movespeed towards the destination of the object.
    public float ORMoveSpeed = 1;   //Movespeed towards the origin of the object.

    public bool ActivateObject;

    public bool IsStairCase;
    public float StairHeightOffset;

    public Stair[] Stairs;

    void Start()
    {
        //Saves the two positions at the start of the game.
        _origin = this.transform.localPosition;

        if (_destination == Vector3.zero)
        {
            _destination = _origin + Displacement;
        }

        if (IsStairCase)
        {
            AssignStairDestination();
        }
    }

    void FixedUpdate()
    {
        if (!IsStairCase)
        {
            if (ActivateObject)
            {
                //Moves towards the set destination.
                this.transform.localPosition = Vector3.Lerp(transform.localPosition, _destination, Time.deltaTime * DESTMoveSpeed);
            }
            else if (!ActivateObject)
            {
                //Moves back to its origin.
                this.transform.localPosition = Vector3.Lerp(transform.localPosition, _origin, Time.deltaTime * ORMoveSpeed);
            }
        }
        else if (IsStairCase)
        {
            if (ActivateObject)
            {
                foreach (Stair stair in Stairs)
                {
                    stair.transform.localPosition = Vector3.Lerp(stair.transform.localPosition, stair.Destination, Time.deltaTime * DESTMoveSpeed);
                }
            }
            else if (!ActivateObject)
            {
                foreach (Stair stair in Stairs)
                {
                    stair.transform.localPosition = Vector3.Lerp(stair.transform.localPosition, stair.Origin, Time.deltaTime * ORMoveSpeed);
                }
            }
        }
    }

    private void AssignStairDestination()
    {
        foreach (Stair stair in Stairs)
        {
            stair.Origin = stair.transform.localPosition;

            stair.Destination = stair.Origin + new Vector3(0, StairHeightOffset, 0);
            StairHeightOffset = StairHeightOffset + 0.18f;
        }
    }
}
