using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicnicBasket : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameEvent LoseEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BugHit()
    {
        health -= 1;

        if(health <= 0)
        {
            LoseEvent.Raise();
        }
    }
}
