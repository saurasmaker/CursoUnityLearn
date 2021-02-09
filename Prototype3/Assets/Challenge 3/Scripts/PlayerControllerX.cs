using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{

    private Rigidbody playerRb;

    [HideInInspector] public bool gameOver;

    [Header("Stage Attributes")]
    public float gravityModifier = 1.5f;

    
    [Header("Player Attributes")]
    [Space(15)]
    public float floatForce;
    [SerializeField] private Transform bounds;

    [Header("Particle Effects")]
    [Space(15)]
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;


    private AudioSource playerAudio;
    [Header("Audio Clips")]
    [Space(15)]
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip touchGround;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Physics.gravity *= gravityModifier;
        

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (transform.position.y < bounds.position.y && Input.GetKey(KeyCode.Space) && !gameOver)
        {
            
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.up * floatForce/3, ForceMode.Impulse);
            playerAudio.PlayOneShot(touchGround, 1.0f);
        }

    }

}
