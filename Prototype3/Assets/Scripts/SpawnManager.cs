using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float startDelay, repeatRate;

    private PlayerController pc;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    void Update()
    {
      
    }

    void SpawnObstacle()
    {
        if (!pc.gameOver)
            Instantiate(obstaclePrefab, transform.position, obstaclePrefab.transform.rotation);
    }
}
