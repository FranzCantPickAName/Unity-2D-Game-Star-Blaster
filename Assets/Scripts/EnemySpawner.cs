using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO currentWave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(
            currentWave.GetEnemyPrefab(0),
            currentWave.GetStartingWaypoint().position,
            Quaternion.identity,
            transform);
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
