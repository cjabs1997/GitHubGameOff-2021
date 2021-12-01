using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private BugBehavior behavior;
    [SerializeField] private BugRuntimeSet bugSet;
    [SerializeField] private SimpleAudioEvent buzzSFX;
    [SerializeField] private GameObject deathEffects;
    [SerializeField] private GameObject landEffects;


    private GameObject target; // What the bug is heading towards, implemenation tbd.

    private Vector2 prevVelocity;
    public float wanderAngle { get; set; } = 0f; // This is pretty gross but I can't think of a better implementation right now

    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }

    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_AudioSource = this.GetComponent<AudioSource>();

        prevVelocity = Vector2.zero;
        target = GameObject.FindGameObjectWithTag("Player");
        bugSet.Add(this);
    }
    private void Start()
    {
        buzzSFX.Play(m_AudioSource);   
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(m_Rigidbody2D.velocity.y, m_Rigidbody2D.velocity.x) * Mathf.Rad2Deg));
    }

    // Where steering behavior stuff will happen, scuffed for now
    private void FixedUpdate()
    {
        Vector2 steering = behavior.CalculateSteeringForce(this, target);
        Vector2 desiredVel = Vector2.ClampMagnitude(m_Rigidbody2D.velocity + steering, behavior.MaxSpeed);
        Vector2 forceToAdd = -behavior.kp * (m_Rigidbody2D.velocity - desiredVel) - behavior.kd * (((m_Rigidbody2D.velocity - prevVelocity) / Time.deltaTime) - steering);

        forceToAdd = Vector2.ClampMagnitude(forceToAdd, behavior.MaxMoveForce);

        // Used to determine desired acceleration for the PD force application
        prevVelocity = m_Rigidbody2D.velocity;

        //Debug.Log("SPEED: " + m_Rigidbody2D.velocity.magnitude);
        //Debug.Log("STEERING: " + steering);
        //Debug.Log("DESIRED VELOCITY: " + desiredVel);
        //Debug.Log("FORCE TO ADD: " + forceToAdd);

        m_Rigidbody2D.AddForce(forceToAdd, ForceMode2D.Force);
    }

    // This is a pretty basic implementation from this. Can adjust this from a design standpoint and figure it out from there.
    private void OnMouseDown()
    {
        health -= 1;
        this.behavior.Hit(1, this); // Maybe we want this to only get called if the bug isn't dead? Leving here for now
        
        if (health <= 0)
        {
            //this.handleDeath();
            Instantiate(deathEffects, this.transform.position, Quaternion.identity);
            behavior.Die(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Have the player lose a health or something, probably also kill the bug
        Debug.Log("PLAYER HIT!");

        if(collision.gameObject.TryGetComponent<PicnicBasket>(out PicnicBasket p))
        {
            p.BugHit();
        }
        Instantiate(landEffects, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject); // Should probably reference something in the behavior instead? This works for the concept tho
    }

    public void UpdateWanderAngle(float randomRange, float angleChange)
    {
        wanderAngle += Random.Range(-randomRange, randomRange) * angleChange - angleChange * 0.5f;
    }

    private void OnDestroy()
    {
        bugSet.Remove(this);
    }
}
