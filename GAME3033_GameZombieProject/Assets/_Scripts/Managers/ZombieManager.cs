using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to a general zombie manager game object on the screen to keep track of all spawner volumes
/// </summary>
public class ZombieManager : MonoBehaviour
{

    public int numZombiesToSpawn;
    public GameObject[] zombiePrefabs;
    public SpawnerVolume[] spawnerVolumes;
    [Range(1, 4)] public float spawnInterval;
    public float spawnTimer;

    GameObject followGameObject;
    // Start is called before the first frame update
    void Start()
    {
        followGameObject = GameObject.FindGameObjectWithTag("Player");
    
        //for (int i = 0; i < numZombiesToSpawn; i++)
        //{
        //    SpawnZombie();
        //}
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnZombie();
            spawnTimer = 0;
        }
    }

    void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        SpawnerVolume spawnVolume = spawnerVolumes[Random.Range(0, spawnerVolumes.Length)];

        if (!followGameObject) return;

        GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);

        zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
    }
}
