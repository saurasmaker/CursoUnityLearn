using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Classes;

namespace Assets.Scripts
{
    public class SaladScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Fruit fruit = new Apple();
            fruit.Chop();
            fruit.SayHello();
            fruit.SayBye();
            ((Apple)fruit).SayBye();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
