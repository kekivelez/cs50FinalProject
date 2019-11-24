using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] List<WaveConfig> WaveConfigs;

    #endregion

    #region Instance variables
    private int firstWave = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        var currentWave = WaveConfigs[firstWave];
        StartCoroutine(SpawnEnemiesInWave(currentWave));
    }

    #region Private Members

    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.EnemyCount; i++)
        {
            var newEnemy = Instantiate(original: waveConfig.EnemyPrefab,
                                       position: waveConfig.GetWaypoints()[0].transform.position,
                                       rotation: Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().WaveConfig = waveConfig;

            yield return new WaitForSeconds(waveConfig.SpawnSpeed); 
        }
    }
    #endregion
}
