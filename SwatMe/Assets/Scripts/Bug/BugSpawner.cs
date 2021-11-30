using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [Tooltip("The positive and negative of this value are used to determing where objects spawn on the x-axis.")]
    [SerializeField] int xSpawnBound;
    [Tooltip("The positive and negative of this value are used to determing where objects spawn on the y-axis.")]
    [SerializeField] int ySpawnBound;

    public Wave wave;

    private List<int> axes = new List<int>();

    void Start()
    {
        axes.Add(xSpawnBound/2); // This feels gormless but it works :P
        axes.Add(-xSpawnBound/2);
        axes.Add(ySpawnBound/2);
        axes.Add(-ySpawnBound/2);

        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        
    }

    public void WaveComplete()
    {
        Debug.Log("YOU DID IT!");
    }

    IEnumerator SpawnWave()
    {
        int axis = 0;
        Vector2 spawnPos = Vector2.zero;

        foreach(Vector2 v in wave.SpawnList)
        {
            for(int i = 0; i < v.y; i++)
            {
                axis = i % 4;

                if (axis <= 1) // Constant x, vary the y
                {
                    spawnPos = new Vector2(axes[axis], Random.Range(axes[3], axes[2]));
                    Instantiate(wave.BugsUsed[(int)v.x], spawnPos, Quaternion.identity);
                }
                else // Vary the x, constant y
                {
                    spawnPos = new Vector2(Random.Range(axes[1], axes[0]), axes[axis]);
                    Instantiate(wave.BugsUsed[(int)v.x], spawnPos, Quaternion.identity);
                }

                yield return new WaitForSeconds(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(xSpawnBound, ySpawnBound, 0));
    }
}
