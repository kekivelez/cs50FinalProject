using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool looping = false;
    [SerializeField] private int firstWave = 0;

    #endregion

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); 
        } while (looping);
    }

    #region Private Members

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

    private IEnumerator SpawnAllWaves()
    {
        for (int i = firstWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }
    #endregion
}
