using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Once I find my examples I'll clean this up and make more sense of it

/// <summary>
/// Base class for Steering Behaviors. These will be used to "steer" entities to make their movement
/// feel more believable and smooth. You should NEVER create an asset from this class, only the extensions
/// of it. 
/// 
/// The goal with these will be to make offshoots of each of the behaviors and then plug them into specific
/// bug Scriptable Objects.
/// </summary>
public abstract class SteeringBehavior : ScriptableObject
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="behavior"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public abstract Vector2 ForceToAdd(Bug bug, BugBehavior behavior, GameObject target);

    #region Secret
/*
public static Vector2 SeekBehavior(EnemyController controller, GameObject target)
{
    // Desired Velocity: Force that guides the character towards its target using the shortest path possible, straight line between them
    Vector2 desiredVel = (target.transform.position - controller.transform.position).normalized * controller.MaxSpeed;
    //Debug.Log("<color=green>SEEKING DESIRED: </color>" + desiredVel + " || " + desiredVel.magnitude);

    // Steering: Pushes the character towards the target, result of the desired velocity subtracted by the current velocity
    Vector2 steering = desiredVel - controller.Rigidbody2D.velocity;
    //Debug.Log("<color=blue>SEEKING STEER: </color>" + steering + " || " + steering.magnitude);

    return steering;
}

public static Vector2 FleeBehavior(EnemyController controller, GameObject target)
{
    // Desired Velocity: Force that guides the character towards its target using the shortest path possible, straight line between them
    Vector2 desiredVel = (controller.transform.position - target.transform.position).normalized * controller.MaxSpeed;
    //Debug.Log("<color=green>FLEEING DESIRED: </color>" + desiredVel + " || " + desiredVel.magnitude);

    // Steering: Pushes the character towards the target, result of the desired velocity subtracted by the current velocity
    Vector2 steering = Vector2.ClampMagnitude(desiredVel - controller.Rigidbody2D.velocity, controller.MaxSteer);
    //Debug.Log("<color=blue>FLEEING STEER: </color>" + steering + " || " + steering.magnitude);

    return steering;
}

public static Vector2 WanderBehavior(EnemyController controller, float circleDistance, float circleRadius, float wanderAngle)
{
    Vector2 circleCenter = controller.Rigidbody2D.velocity.normalized * circleDistance;
    Vector2 displacementForce = Vector2.right * circleRadius;

    float displacementMag = displacementForce.magnitude;

    displacementForce = new Vector2(Mathf.Cos(wanderAngle) * displacementMag, Mathf.Sin(wanderAngle) * displacementMag);

    Vector2 steering = circleCenter + displacementForce;

    return steering;
}

public static Vector2 PursuitBehavior(EnemyController controller, GameObject target)
{
    float predictionWindow = (target.transform.position - controller.transform.position).magnitude / controller.MaxSpeed;

    Vector2 steering = (Vector2)target.transform.position + target.GetComponent<Rigidbody2D>().velocity * predictionWindow;
    steering = steering - (Vector2)controller.transform.position;


    return steering;
}

public static Vector2 EvadeBehavior(EnemyController controller, GameObject target)
{
    float predictionWindow = (target.transform.position - controller.transform.position).magnitude / controller.MaxSpeed;

    Vector2 steering = (Vector2)target.transform.position + target.GetComponent<Rigidbody2D>().velocity * predictionWindow;
    steering = (Vector2)controller.transform.position - steering;

    return steering;
}

public static Vector2 CollisionAvoidanceBehavior(EnemyController controller, float maxSeeAhead, float maxAvoidanceForce, LayerMask collisionLayer)
{
    Vector2 steering = Vector2.zero;

    float theta = 30;
    theta = theta * (Mathf.PI / 180);
    Vector2 v = controller.transform.forward;
    Vector2 left = new Vector2(Mathf.Cos(-theta) * v.x - Mathf.Sin(-theta) * v.y,
        Mathf.Sin(-theta) * v.x + Mathf.Cos(-theta) * v.y);
    Vector2 right = new Vector2(Mathf.Cos(theta) * v.x - Mathf.Sin(theta) * v.y,
        Mathf.Sin(theta) * v.x + Mathf.Cos(theta) * v.y);

    // Gormless George implementation for right now but just wanted to check out how it would work
    RaycastHit2D hit1 = Physics2D.Raycast((Vector2)controller.transform.position, primaryDirection, maxSeeAhead, collisionLayer);
    RaycastHit2D hit2 = Physics2D.Raycast((Vector2)controller.transform.position, rightDirection + new Vector2(-0.1f, 0), maxSeeAhead, collisionLayer);
    RaycastHit2D hit3 = Physics2D.Raycast((Vector2)controller.transform.position, leftDirection, maxSeeAhead, collisionLayer);
    Debug.DrawRay((Vector2)controller.transform.position, primaryDirection, Color.green);
    Debug.DrawRay((Vector2)controller.transform.position, rightDirection, Color.blue);
    Debug.DrawRay((Vector2)controller.transform.position, leftDirection, Color.red);
    if (hit1)
    {
        float dynamicLength = controller.Rigidbody2D.velocity.magnitude / controller.MaxSpeed;
        Vector2 ahead = ((Vector2)controller.transform.position + controller.Rigidbody2D.velocity).normalized * dynamicLength;
        steering = ahead - (Vector2)hit1.transform.position;
        steering = steering.normalized * maxAvoidanceForce;
    }
    else if(hit2)
    {
        float dynamicLength = controller.Rigidbody2D.velocity.magnitude / controller.MaxSpeed;
        Vector2 ahead = ((Vector2)controller.transform.position + controller.Rigidbody2D.velocity).normalized * dynamicLength;
        steering = ahead - (Vector2)hit2.transform.position;
        steering = steering.normalized * maxAvoidanceForce;
    }
    else if(hit3)
    {
        float dynamicLength = controller.Rigidbody2D.velocity.magnitude / controller.MaxSpeed;
        Vector2 ahead = ((Vector2)controller.transform.position + controller.Rigidbody2D.velocity).normalized * dynamicLength;
        steering = ahead - (Vector2)hit3.transform.position;
        steering = steering.normalized * maxAvoidanceForce;
    }

    return steering;
}


public static Vector2 FollowBehavior(EnemyController controller, GameObject target, float followDistance)
{
    // The point behind the object that we want to get to.
    Vector2 followPoint = target.GetComponent<Rigidbody2D>().velocity * -1;
    followPoint = followPoint.normalized * followDistance;
    followPoint = (Vector2)target.transform.position + followPoint;

    // The direction we ideally want to travel
    Vector2 desiredVel = (followPoint - (Vector2)controller.transform.position).normalized * controller.MaxSpeed;

    // How much to adjust our current trajectory to meet the desired velocity
    Vector2 steering = Vector2.ClampMagnitude(desiredVel - controller.Rigidbody2D.velocity, controller.MaxSteer);


    return steering;
}
*/
#endregion
}
