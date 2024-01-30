using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> waves; // List to hold multiple waves
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int activeEnemies = 0;
    private float lastSpawnTime;

    void Start()
    {
        if (waves.Count > 0)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var set in wave.spawnerData.enemySets)
        {
            for (int i = 0; i < set.amount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-5f, 5f));
                GameObject enemy = Instantiate(set.enemyPrefab, transform.position + randomOffset, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.OnDeath += OnEnemyDeath;
                    activeEnemies++; // Increment activeEnemies for each enemy spawned
                    Debug.Log("Enemy script subscribed");
                }

                if (i == set.amount - 1) // If it's the last enemy of the set
                {
                    lastSpawnTime = Time.time; // Update the last spawn time
                }
                yield return new WaitForSeconds(0.1f); // Wait before spawning the next enemy
            }

            // Wait for either all enemies in the wave to be defeated or the time interval to pass before spawning the next set
            yield return new WaitUntil(() => activeEnemies == 0 || TimePassedSinceLastSpawn(wave.spawnerData.timeBetweenSets));
        }

        // Wait for all enemies in the wave to be defeated before starting the next wave
        yield return new WaitUntil(() => activeEnemies == 0);

        currentWaveIndex++;
        if (currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex])); // Start the next wave
        }
    }


    private void OnEnemyDeath(Enemy enemy)
    {
        activeEnemies--;
        if (enemy != null)
        {
            enemy.OnDeath -= OnEnemyDeath;
            Debug.Log("Enemy script unsubscribed");
        }
    }

    bool TimePassedSinceLastSpawn(float timeBetweenSets)
    {
        return Time.time >= lastSpawnTime + timeBetweenSets;
    }

}
