using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave", fileName ="Wave")]
public class Wave : ScriptableObject
{
    [Tooltip("At what interval the spawner will spawn the bugs.")]
    [SerializeField] protected float spawnInterval = 1f;
    public float SpawnInterval { get { return spawnInterval; } }

    [Tooltip("The x value represents what bug to spawn, index of the bugsUsed list. The y value is the quantity. Presently it will " +
        "spawn in the order listed here. Can probs change that later...")]
    [SerializeField] protected List<Vector2> spawnList;
    public List<Vector2> SpawnList { get { return spawnList; } }

    [Tooltip("List of what bugs to spawn.")]
    [SerializeField] protected List<GameObject> bugsUsed;
    public List<GameObject> BugsUsed { get { return bugsUsed; } }


}
