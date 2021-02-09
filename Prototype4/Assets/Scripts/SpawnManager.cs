using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private int enemies = 1;
    public float spawnRange;
    [HideInInspector] public int currentEnemies = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemies <= 0)
        {
            ++enemies;
            currentEnemies = enemies;
            for(int i = 0; i < currentEnemies; ++i)
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
        
    }

    Vector3 GenerateSpawnPosition()
    {        
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        
        return new Vector3(spawnPosX, 0.5f, spawnPosZ);
    }
}
