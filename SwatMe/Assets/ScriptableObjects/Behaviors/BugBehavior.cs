using UnityEngine;

/// <summary>
/// Should hold all the behavior that all bugs rely on. Currently working on that list....
/// 
/// </summary>

[CreateAssetMenu(menuName ="Behavior")]
public class BugBehavior : ScriptableObject
{
    public virtual void Initialize(){ } //Not sure what to do with this for right now...

    public void Die(Bug bug)
    {
        Debug.Log("Butts.");
        Destroy(bug.gameObject);
    }

    public void Hit(float damage, Bug bug)
    {
        
    }
}
