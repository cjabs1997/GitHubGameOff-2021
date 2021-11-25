using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private BugBehavior behavior;

    [SerializeField] private Transform goal; // What the bug is heading towards, implemenation tbd.
    // For now setting as Serialized so I can manually set for testing.

    // This is a pretty basic implementation from this. Can adjust this from a design standpoint and figure it out from there.
    private void OnMouseDown()
    {
        health -= 1;
        this.behavior.Hit(1, this);

        
        if (health <= 0)
        {
            //this.handleDeath();
            behavior.Die(this);
        }
    }

    private void handleDeath()
    {
        Destroy(this.gameObject);
    }
}
