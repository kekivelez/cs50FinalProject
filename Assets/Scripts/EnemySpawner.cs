using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool looping = false;
    [SerializeField] private int firstWave = 0;
    [SerializeField] private int bossWave;


    private System.Random randomGen;
    #endregion

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Set the boss wave as the last in the list
        bossWave = waveConfigs.Count - 1;

        // Create a random number generator and seed it with UTC time
        randomGen = new System.Random((int)System.DateTime.UtcNow.Ticks);
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    #region Private Members
    /// <summary>
    /// Coroutine called to spawn all the enemies in a given wave
    /// </summary>
    /// <param name="waveConfig">The wave configuration containing the enemy data</param>
    /// <returns></returns>
    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.EnemyCount; i++)
        {
            // Instantiate a new enemy
            var newEnemy = Instantiate(original: waveConfig.EnemyPrefab,
                                       position: waveConfig.GetWaypoints()[0].transform.position,
                                       rotation: Quaternion.identity);

            // Get the script component of the newly instantiated enemy and associate the wave config with it
            newEnemy.GetComponent<EnemyPathing>().WaveConfig = waveConfig;

            yield return new WaitForSeconds(waveConfig.SpawnSpeed); 
        }
    }

    /// <summary>
    /// Coroutine called to spawn all defined enemy waves
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnAllWaves()
    {
        // Randomly determine how many waves will happen before the boss wave can spawn (min 2 waves)
        int wavesBeforeBoss = Random.Range(2, waveConfigs.Count - 1);
        for (int i = firstWave; i < wavesBeforeBoss; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }

        // approx 30% chance for boss to spawn after all waves are done
        bool spawnBoss = randomGen.Next(0, 10) <= 3 ? true : false;
        if (spawnBoss) { yield return StartCoroutine(SpawnEnemiesInWave(waveConfigs[bossWave])); }

    }
    #endregion
}
