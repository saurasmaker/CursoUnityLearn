using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 13;
    private float maxSpeed = 17;
    private float maxTorque = 10;
    private float xRange = 7;
    private float ySpawnPos = 0;
    private float zSpawnPos = -6;

    private GameManager gameManager;
    private ParticleSystem particleSystem;
    private Transform downBound;
    private bool canClick = true;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        downBound = GameObject.Find("DownBound").transform;

        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        particleSystem.Pause();

        gameManager.UpdateScore();
    }

    void Update()
    {
        if (transform.position.y < downBound.position.y)
        {
            Destroy(gameObject);
            if (!gameObject.CompareTag("BadTarget") && !gameManager.isGameOver && canClick)
            {
                gameManager.lifes--;
                gameManager.UpdateScore();               
            }
        }
    }

    private void OnMouseDown()
    {
        if (canClick && !gameManager.isGameOver)
        {
            canClick = false;
            
            particleSystem.Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            if (CompareTag("GoodTarget1"))
                ++gameManager.score;
            else if (CompareTag("GoodTarget2"))
                gameManager.score += 2;
            else if (CompareTag("GoodTarget3"))
                gameManager.score += 3;
            else if (CompareTag("BadTarget"))
                gameManager.lifes -= 3;

            gameManager.UpdateScore();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, zSpawnPos);
    }
}
