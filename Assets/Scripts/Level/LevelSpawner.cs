using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private int SpawnHeigtIncrements = 14;
    private int LastSpawnHeigt = 14;

    [Header("Levels")]
    [SerializeField] private GameObject[] easyLevels;
    [SerializeField] private GameObject[] mediumLevels;
    [SerializeField] private GameObject[] hardLevels;


    public static LevelSpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;       
        }
    }
    public void SpawnLevel(Difficulty difficulty)
    {
        GameObject[] levelsToSpawn = difficulty switch
        {
            Difficulty.easy => easyLevels,
            Difficulty.medium => mediumLevels,
            Difficulty.hard => hardLevels,
            _ => easyLevels
        };

        var prefab = levelsToSpawn[Random.Range(0, levelsToSpawn.Length)];
        Instantiate(prefab, new Vector3(0, LastSpawnHeigt, 0), Quaternion.identity);
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        LastSpawnHeigt += SpawnHeigtIncrements;
    }
}
