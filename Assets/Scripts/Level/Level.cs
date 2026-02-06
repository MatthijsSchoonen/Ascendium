using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("SpawnPoints")]
    [SerializeField] private GameObject[] enemySpawnPoints;
    [SerializeField] private GameObject[] itemSpawnPoints;
    [SerializeField] private GameObject[] hazerdSpawnPoints;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private GameObject[] hazerdPrefabs;

    [Header("SpawnChances")]
    [SerializeField] private float enemySpawnChance = 0.7f;
    [SerializeField] private float itemSpawnChance = 0.5f;
    [SerializeField] private float hazerdSpawnChance = 0.4f;

    
    private void SpawnEnemy()
    {

    }

    private void SpawnItem()
    {
    }

    private void SpawnHazerd()
    {
    }
}
