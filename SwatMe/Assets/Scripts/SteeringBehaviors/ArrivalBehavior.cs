using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrivalBehavior", menuName = "SteeringBehavior/ArrivalBehavior")]
public class ArrivalBehavior : SteeringBehavior
{
    [SerializeField] protected float slowRadius;

    public override Vector2 ForceToAdd(Bug bug, BugBehavior behavior, GameObject target)
    {
        // Desired Velocity: Force that guides the character towards its target using the shortest path possible, straight line between them
        Vector2 desiredVel = target.transform.position - bug.transform.position;

        float distance = desiredVel.magnitude;

        // Modify desiredVel based on distance from the target
        // Within the slowRadius
        if (distance < slowRadius)
        {
            desiredVel = desiredVel.normalized * behavior.MaxSpeed * (distance / slowRadius);
        }
        else // Outside the slowRadius
        {
            desiredVel = desiredVel.normalized * behavior.MaxSpeed;
        }

        Vector2 steering = desiredVel - bug.Rigidbody2D.velocity;
        //Debug.Log("<color=blue>SEEKING STEER: </color>" + steering + " || " + steering.magnitude);

        return steering;
    }
}
