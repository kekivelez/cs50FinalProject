using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private bool _looping = false;
    [SerializeField] private int _firstWave = 0;

    #endregion

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); 
        } while (_looping);
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
        for (int i = _firstWave; i < _waveConfigs.Count; i++)
        {
            var currentWave = _waveConfigs[i];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }
    #endregion
}
