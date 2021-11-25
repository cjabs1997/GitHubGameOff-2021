using UnityEngine;

/// <summary>
/// Should hold all the behavior that all bugs rely on. Currently working on that list....
/// 
/// </summary>

[CreateAssetMenu(menuName ="Behavior")]
public class BugBehavior : ScriptableObject
{
    [SerializeField] private SteeringBehavior steeringBehavior;

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
}
