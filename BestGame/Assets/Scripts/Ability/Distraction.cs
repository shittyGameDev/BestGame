using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    private void Start()
    {
        FindAndRetrieveEnemyMovements();
    }

    // Find all enemies in the scene and retrieve their EnemyMovement component TODO: Radius
    private void FindAndRetrieveEnemyMovements()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();

            if (enemyMovement != null)
            {
                // Set the enemy to be distracted
                enemyMovement.isDistracted = true;
                Debug.Log("Enemy is distracted");
            }
        }
    }
}
