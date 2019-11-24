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
    public GameObject EnemyPrefab => this.enemyPrefab;

    public float SpawnSpeed => this.spawnSpeed;

    public float SpawnSpeedOffset => this.spawnSpeedOffset;

    public float MoveSpeed => this.moveSpeed;

    public int EnemyCount => this.enemyCount;

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
