using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //Attributes
    public Transform[] points;
    public float speed = 1f;
    public bool loop;

    private Vector3[] updatedPoints;
    private int count = 0;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        updatedPoints = new Vector3[points.Length];

        for (int i = 0; i < points.Length; ++i)
        {
            if (points[i] != null)
            {
                updatedPoints[i] = points[i].position;
            }
        }

        transform.position = updatedPoints[0];
    }

    void FixedUpdate()
    {

        if (!ended && updatedPoints[Mathf.Abs(count)] != null && updatedPoints[Mathf.Abs(count + 1)] != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, updatedPoints[Mathf.Abs(count + 1)], fixedSpeed);

            if (transform.position == updatedPoints[Mathf.Abs(count + 1)])
            {
                Debug.Log(count);
                ++count;

                if (loop)
                {
                    if (count == points.Length - 1)
                        count = -count;
                }
                else
                    ended = true;
            }
        }

    }


    private void OnDrawGizmosSelected()
    {
        /*
         * Este método sirve para dibujar la trayectoria que va a seguir
         * la plataforma cuando se mueva. Esto es solo para el entorno 
         * de desarrollo. In Game esta línea no se verá. 
         */
        if (points != null)
            for (int i = 0; i < (points.Length - 1); ++i)
                if (points[i] != null && points[i + 1] != null)
                {
                    Gizmos.color = Color.red;

                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
                    Gizmos.DrawSphere(points[i].position, 0.15f);
                    Gizmos.DrawSphere(points[i + 1].position, 0.15f);
                }

    }
}
