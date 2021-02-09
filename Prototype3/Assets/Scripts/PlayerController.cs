using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;

    [HideInInspector] public bool isOnGround, gameOver;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!gameOver && isOnGround && Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = true;

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            Debug.Log("Game Over!");
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Jump_trig");
            isOnGround = false;
        }
    }
}
