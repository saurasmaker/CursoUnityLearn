using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string myMessage;

    // Start is called before the first frame update
    void Start()
    {
        print(myMessage);
    }

    // Update is called once per frame
    void Update()
    {
        print("Hello World in Update");
    }
}
