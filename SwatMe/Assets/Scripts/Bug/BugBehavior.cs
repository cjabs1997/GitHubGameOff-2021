using UnityEngine;

/// <summary>
/// Should hold all the behavior that all bugs rely on. Currently working on that list....
/// 
/// </summary>

[CreateAssetMenu(menuName ="Behavior")]
public class BugBehavior : ScriptableObject
{
    [SerializeField] private SteeringBehavior steeringBehavior;

    [Tooltip("The maximum speed the object can reach while using this behavior.")]
    [SerializeField] protected float maxSpeed;
    public float MaxSpeed { get { return maxSpeed; } }
    [Tooltip("The maximum force that can be applied to steer the object in a given tick.")]
    [SerializeField] protected float maxSteer; // Influences how much effect the steering force has on the object.
    public float MaxSteer { get { return maxSteer; } }
    [Tooltip("The maximum total force that can be applied to the object in a given tick.")]
    [SerializeField] protected float maxMoveForce; // Influences how sharp a change in velocity can be.
    public float MaxMoveForce { get { return maxMoveForce; } }

    [Header("PD Info")]
    [SerializeField] protected float PD_kp = 0.5f; // The higher this value is the faster the object will accelerate. However, larger values will result in more oscillations when reaching the top speed.
    public float kp { get { return PD_kp; } }
    [SerializeField] protected float PD_kd = 0.001f; // Adjust this value as you adjust kp, increasing it to help eliminate the oscillations.
    public float kd { get { return PD_kd; } }


    public virtual void Initialize(){ } //Not sure what to do with this for right now...

    public void Die(Bug bug)
    {
        Debug.Log("Butts.");
        Destroy(bug.gameObject);
    }

    /// <summary>
    /// Called when the bug is clicked on. Ideally this should probably only play effects and such. I think tracking the
    /// data should be done locally on the bug itself but that can be adjusted.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="bug"></param>
    public void Hit(float damage, Bug bug)
    {
        // Play some particles or something??? LUL
    }

    public Vector2 CalculateSteeringForce(Bug bug, GameObject target)
    {
        // This looks a little silly but the strength of these behaviors is that we can add them together to get more complex
        // movement patterns. Hence the weirdness.
        Vector2 steering = Vector2.zero;
        steering += steeringBehavior.ForceToAdd(bug, this, target);

        steering = Vector2.ClampMagnitude(steering, maxMoveForce);
        return steering;
    }
}
