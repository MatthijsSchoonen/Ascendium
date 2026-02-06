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

    bool entered = false;


    private void Start()
    {
        SpawnEnemy();
        SpawnItem();
        SpawnHazerd();
    }
    private void SpawnEnemy()
    {
        foreach (var spawnPoint in enemySpawnPoints)
        {
            if (Random.value < enemySpawnChance)
            {
                var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }

    }

    private void SpawnItem()
    {
        foreach (var spawnPoint in itemSpawnPoints)
        {
            if (Random.value < itemSpawnChance)
            {
                var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private void SpawnHazerd()
    {
        foreach (var spawnPoint in hazerdSpawnPoints)
        {
            if (Random.value < hazerdSpawnChance)
            {
                var prefab = hazerdPrefabs[Random.Range(0, hazerdPrefabs.Length)];
                Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!entered && collision.CompareTag("Player"))
        {
            entered = true;
            GameManager.Instance.IncreaseScore();
        }           
    }
}
