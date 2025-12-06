using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO currentWave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(
            currentWave.GetEnemyPrefab(0),
            currentWave.GetStartingWaypoint().position,
            Quaternion.identity,
            transform);

            yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
