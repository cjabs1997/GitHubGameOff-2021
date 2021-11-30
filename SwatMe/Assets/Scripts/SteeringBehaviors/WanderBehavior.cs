using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WanderBehavior", menuName = "SteeringBehavior/WanderBehavior")]
public class WanderBehavior : SteeringBehavior
{
    [SerializeField] private float circleDistance;
    [SerializeField] private float circleRadius;
    [Tooltip("Affects how much the wander angle can change each tick.")]
    [SerializeField] private float angleChange;
    [Tooltip("Scale for angle change. A random value between it and the negative of itself are multiplied by the angleChange value.")]
    [SerializeField] private float randomRange;


    public override Vector2 ForceToAdd(Bug bug, BugBehavior behavior, GameObject target)
    {
        float wanderAngle = Mathf.Atan2(bug.Rigidbody2D.velocity.y, bug.Rigidbody2D.velocity.x);
        wanderAngle += Random.Range(-randomRange, randomRange) * angleChange - angleChange;


        Vector2 circleCenter = bug.Rigidbody2D.velocity.normalized * circleDistance;
        Vector2 displacementForce = Vector2.right * circleRadius;

        float displacementMag = displacementForce.magnitude;

        displacementForce = new Vector2(Mathf.Cos(wanderAngle) * displacementMag, Mathf.Sin(wanderAngle) * displacementMag);

        Vector2 steering = circleCenter + displacementForce;

        return steering;
    }
}
