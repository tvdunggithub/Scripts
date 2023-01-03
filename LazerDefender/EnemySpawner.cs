using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

***Script này để spawn ra enemy ***

-Gắn list wavePrefab vào để spawn ra enemy trong wave dc gắn

*/
public class EnemySpawner : MonoBehaviour
{
    WaveConfigSO currentWave;
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    public bool isLooping;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }


    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);  
            }
        }while(isLooping);
    }
}

