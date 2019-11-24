using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    #region Config Parameters
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject pathPrefab;
    [SerializeField] public float spawnSpeed = 0.5f;
    [SerializeField] public float spawnSpeedOffset = 0.3f;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public int enemyCount = 5;

    #endregion

    #region Public Members
    /// <summary>
    /// The enemy object
    /// </summary>
    public GameObject EnemyPrefab => this.enemyPrefab;

    /// <summary>
    /// The speed at which the members of the wave spawn
    /// </summary>
    public float SpawnSpeed => this.spawnSpeed;

    /// <summary>
    /// Adds a displacement to the spawn speed to allow a sense of irregularity
    /// between spawns
    /// </summary>
    public float SpawnSpeedOffset => this.spawnSpeedOffset;

    /// <summary>
    /// The movement speed of the enemies in the wave
    /// </summary>
    public float MoveSpeed => this.moveSpeed;

    /// <summary>
    /// How many enemies per wave
    /// </summary>
    public int EnemyCount => this.enemyCount;

    /// <summary>
    /// Gets a list of waypoints based on the path object
    /// </summary>
    /// <returns>A list of waypoints for the enemy to follow</returns>
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform waypointTransform in pathPrefab.transform)
        {
            waveWaypoints.Add(waypointTransform);
        }

        return waveWaypoints;
    }
    #endregion


}
