using UnityEngine;

public abstract class BugBehavior : ScriptableObject
{
    public virtual void Initialize(){ }
    public abstract void Act(Bug bug);

}
