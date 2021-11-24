using UnityEngine;

[CreateAssetMenu(menuName="Bug")]
public class Bug : ScriptableObject
{    
    public float health = 1f;
    public void onHit(){ 
        health -= 1;
        if(health <= 0){
            this.handleDeath();
        }
    }
    private void handleDeath(){
        Destroy(this);
    }
    public BugBehavior behavior;
}
