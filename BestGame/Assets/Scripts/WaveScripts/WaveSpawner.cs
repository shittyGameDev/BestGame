using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> waves; // List to hold multiple waves
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int activeEnemies = 0;
    private float lastSpawnTime;

    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>().Where(t => t != this.transform).ToArray();

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points found");
            return;
        }

        Debug.Log("Number of spawn points: " + spawnPoints.Length);

        StartCoroutine(CheckForGoldMine());
    }

    IEnumerator CheckForGoldMine()
    {
        while (true)
        {
            TowerBase[] goldMines = GameObject.FindGameObjectsWithTag("GoldMine")
                .Select(obj => obj.GetComponent<TowerBase>())
                .Where(goldMine => goldMine != null && goldMine.IsActive)
                .ToArray();

            if (goldMines.Length > 0)
            {
                // Goldmine is now active, start spawning waves
                if (waves.Count > 0)
                {
                    GameManager.Instance.StartGame();
                    yield return new WaitForSeconds(GameManager.Instance.countDown); // Wait for the game to start
                    if (GameManager.Instance.state == GameState.Playing)
                    {
                        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                    }
                }
                yield break;
            }

            yield return new WaitForSeconds(1f); // Check every second
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var set in wave.spawnerData.enemySets)
        {
            for (int i = 0; i < set.amount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = Instantiate(set.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (enemyScript != null)
                {
                    enemyScript.OnDeath += OnEnemyDeath;
                    activeEnemies++;
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

    bool IsGoldTowerActive()
    {
        return GameObject.FindGameObjectsWithTag("GoldMine").Length > 0;
    }

}
