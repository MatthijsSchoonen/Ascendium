using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private int SpawnHeigtIncrements = 14;
    private int LastSpawnHeigt = 0;

    public void SpawnLevel()
    {
        LastSpawnHeigt += SpawnHeigtIncrements;
    }
}
