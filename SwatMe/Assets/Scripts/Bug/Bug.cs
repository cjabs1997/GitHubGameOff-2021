using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private BugBehavior behavior;

    [SerializeField] private Transform goal; // What the bug is heading towards, implemenation tbd.
    // For now setting as Serialized so I can manually set for testing.

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(m_Rigidbody2D.velocity.y, m_Rigidbody2D.velocity.x) * Mathf.Rad2Deg));
    }

    // Where steering behavior stuff will happen, scuffed for now
    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = (goal.position - this.transform.position).normalized * 2; 
    }

    // This is a pretty basic implementation from this. Can adjust this from a design standpoint and figure it out from there.
    private void OnMouseDown()
    {
        health -= 1;
        this.behavior.Hit(1, this); // Maybe we want this to only get called if the bug isn't dead? Leving here for now
        
        if (health <= 0)
        {
            //this.handleDeath();
            behavior.Die(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Have the player lose a health or something, probably also kill the bug
        Debug.Log("PLAYER HIT!");
        Destroy(this.gameObject); // Should probably reference something in the behavior instead? This works for the concept tho
    }
}
