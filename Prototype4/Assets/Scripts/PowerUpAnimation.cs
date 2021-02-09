using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animation();
        
    }

    private void Animation()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
