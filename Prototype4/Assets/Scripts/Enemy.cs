using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Transform boundY;

    private Rigidbody rb;
    private GameObject player;
    private SpawnManager sm;

    // Start is called before the first frame update
    void Start()
    {
        boundY = GameObject.Find("DownBound").transform;
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rb.AddForce(lookDirection * speed * Time.deltaTime);

        if (transform.position.y < boundY.position.y)
        {
            --sm.currentEnemies;
            Destroy(gameObject);
        }
    }

    
}
