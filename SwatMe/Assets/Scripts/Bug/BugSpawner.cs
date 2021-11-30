using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [Tooltip("The positive and negative of this value are used to determing where objects spawn on the x-axis.")]
    [SerializeField] int xSpawnBound;
    [Tooltip("The positive and negative of this value are used to determing where objects spawn on the y-axis.")]
    [SerializeField] int ySpawnBound;

    public GameObject fly;

    private List<int> axes = new List<int>();

    void Start()
    {
        axes.Add(xSpawnBound/2); // This feels gormless but it works :P
        axes.Add(-xSpawnBound/2);
        axes.Add(ySpawnBound/2);
        axes.Add(-ySpawnBound/2);


        Debug.Log(axes[0]);
        Debug.Log(axes[1]);
        Debug.Log(axes[2]);
        Debug.Log(axes[3]);

        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnWave()
    {
        int count = 0;
        int axis = 0;
        Vector2 spawnPos = Vector2.zero;

        while (count < 10)
        {
            axis = count % 4;

            Debug.Log("AXIS: " + axis);

            if(axis <= 1) // Constant x, vary the y
            {
                spawnPos = new Vector2(axes[axis], Random.Range(axes[3], axes[2]));
                Instantiate(fly, spawnPos, Quaternion.identity);
            }
            else // Vary the x, constant y
            {
                spawnPos = new Vector2(Random.Range(axes[1], axes[0]), axes[axis]);
                Instantiate(fly, spawnPos, Quaternion.identity);
            }

            Debug.Log("SPAWN POS:" + spawnPos);

            count += 1;

            yield return new WaitForSeconds(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(xSpawnBound, ySpawnBound, 0));
    }
}
