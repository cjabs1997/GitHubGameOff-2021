using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{
    [SerializeField] private Bug bug;

    private void Start(){
        Debug.Log(bug.health);
    }
}
