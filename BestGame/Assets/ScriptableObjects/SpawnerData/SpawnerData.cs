using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 1)]
public class SpawnerData : ScriptableObject
{
    [System.Serializable]
    public class EnemySet {
        public GameObject enemyPrefab;
        public int amount;
    }

    public List<EnemySet> enemySets;
    public float timeBetweenSets = 5f;
}
