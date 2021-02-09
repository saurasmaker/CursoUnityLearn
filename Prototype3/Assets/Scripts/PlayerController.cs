using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem dirtParticles;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;

    public AudioClip jumpSound, crashSound;


    [HideInInspector] public bool isOnGround, gameOver;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticles.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            dirtParticles.Stop();
            explosionParticles.Play();
            audioSource.PlayOneShot(crashSound, 1.0f);
            Debug.Log("Game Over!");
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticles.Stop();
            animator.SetTrigger("Jump_trig");
            isOnGround = false;
        }
    }
}
