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

    #endregion

    // Start is called before the first frame update
    IEnumerator Start()
    {
        bossWave = waveConfigs.Count - 1;
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
        int wavesBeforeBoss = Random.Range(2, waveConfigs.Count - 1);
        for (int i = firstWave; i < wavesBeforeBoss; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
        yield return StartCoroutine(SpawnEnemiesInWave(waveConfigs[bossWave]));

    }
    #endregion
}
