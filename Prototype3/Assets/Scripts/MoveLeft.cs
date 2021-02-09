using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private Vector3 leftBound;

    private PlayerController pc;

    private void Awake()
    {
        leftBound = GameObject.Find("LeftBound").transform.position;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(!pc.gameOver)
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < leftBound.x)
            Destroy(gameObject);
    }

    
}
