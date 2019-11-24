using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    #region Config Parameters
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _spawnSpeed = 0.5f;
    [SerializeField] private float _spawnSpeedOffset = 0.3f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _enemyCount = 5;

    #endregion

    #region Public Members
    /// <summary>
    /// The enemy object
    /// </summary>
    public GameObject EnemyPrefab => this._enemyPrefab;

    /// <summary>
    /// The speed at which the members of the wave spawn
    /// </summary>
    public float SpawnSpeed => this._spawnSpeed;

    /// <summary>
    /// Adds a displacement to the spawn speed to allow a sense of irregularity
    /// between spawns
    /// </summary>
    public float SpawnSpeedOffset => this._spawnSpeedOffset;

    /// <summary>
    /// The movement speed of the enemies in the wave
    /// </summary>
    public float MoveSpeed => this._moveSpeed;

    /// <summary>
    /// How many enemies per wave
    /// </summary>
    public int EnemyCount => this._enemyCount;

    /// <summary>
    /// Gets a list of waypoints based on the path object
    /// </summary>
    /// <returns>A list of waypoints for the enemy to follow</returns>
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform waypointTransform in _pathPrefab.transform)
        {
            waveWaypoints.Add(waypointTransform);
        }

        return waveWaypoints;
    }
    #endregion


}
