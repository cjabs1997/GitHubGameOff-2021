using UnityEngine;

[CreateAssetMenu(menuName="Bug")]
public class Bug : ScriptableObject
{    
    public float health = 1f;
    public void onHit(){ 
        health -= 1;
    }
    public BugBehavior behavior;
}
