using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public SpawnerData spawnerData;
    private int currentSetIndex = 0;
    [SerializeField] private int activeEnemies = 0;
    private float lastSpawnTime;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        foreach (var set in spawnerData.enemySets)
        {
            activeEnemies = set.amount;

            for (int i = 0; i < set.amount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-5f, 5f));
                GameObject enemy = Instantiate(set.enemyPrefab, transform.position + randomOffset, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.OnDeath += OnEnemyDeath;
                    Debug.Log("Enemy script subscribed");
                }
                if (i == set.amount - 1) // If it's the last enemy of the set
                {
                    lastSpawnTime = Time.time; // Update the last spawn time
                }
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitUntil(() => activeEnemies == 0 || TimePassedSinceLastSpawn(spawnerData.timeBetweenSets));
            currentSetIndex++;
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
