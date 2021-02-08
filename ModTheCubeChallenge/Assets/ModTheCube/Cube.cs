using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ModTheCube
{
    public class Cube : MonoBehaviour
    {

        #region Colors
        
        
        [Header("Color")]
        [SerializeField] [Range(0f, 1f)] private float colorSpeed;
        [SerializeField] private Color[] colors;

        private MeshRenderer meshRenderer;
        private int colorIndex = 0;
        private float tc = 0f;
        private int len;
        #endregion

        

        #region Rotate
        [Header("Rotate")]
        [SerializeField] [Range(0f, 100f)] private float rotationSpeed;
        [SerializeField] private Vector3[] rotations;
        #endregion


        #region Scale
        [Header("Scale")]
        [SerializeField] [Range(0f, 1f)] private float scaletionSpeed;
        [SerializeField] [Range(0f, 100f)] private float scaletion;
        private float ts = 0f;
        private Vector3 auxScale;
        private bool growing = true, switchScale = false;
        #endregion


        #region Position
        [Header("Position")]
        public Transform[] points;
        public float speed = 1f;
        public bool loop;

        private Vector3[] updatedPoints;
        private int count = 0;
        private bool ended = false;
        #endregion


        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            len = colors.Length;

            auxScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * scaletion;

            updatedPoints = new Vector3[points.Length];
            for (int i = 0; i < points.Length; ++i)
                if (points[i] != null)
                    updatedPoints[i] = points[i].position;
                         
            transform.position = updatedPoints[0];
        }

        private void Update()
        {
            ChangeColor();
            Rotate();
            Scale();
            Position();
        }

        private void ChangeColor()
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, colors[colorIndex], colorSpeed * Time.deltaTime);
            tc = Mathf.Lerp(tc, 1f, colorSpeed * Time.deltaTime);
            if (tc > .8f)
            {
                tc = 0f;
                ++colorIndex;
                colorIndex = (colorIndex >= len) ? 0 : colorIndex;
            }
        }

        private void Rotate()
        {
            for (int i = 0; i < rotations.Length; ++i)
            {
                transform.Rotate(rotations[i], rotationSpeed * Time.deltaTime);
            }
        }

        private void Scale()
        {
            if (switchScale)
            {
                switchScale = false;
                if (growing)
                {
                    growing = false;
                    auxScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) / scaletion;
                }
                else
                {
                    growing = true;
                    auxScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * scaletion;
                }

            }

            transform.localScale = Vector3.Lerp(transform.localScale, auxScale, scaletionSpeed * Time.deltaTime);
            ts = Mathf.Lerp(ts, 1f, scaletionSpeed * Time.deltaTime);
            if (ts > .8f)
            {
                ts = 0f;
                switchScale = true;
            }
        }

        private void Position()
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
}

