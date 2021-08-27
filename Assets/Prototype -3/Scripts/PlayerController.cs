using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float gravityModifier;

    public bool doubleSpeed = false;
    public  bool isOnGround;
    public bool isDoubleJumpUsed = false;
    public bool gameOver = false;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;
    AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;


    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Physics.gravity = Physics.gravity * gravityModifier
        Physics.gravity *= gravityModifier;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {
        Jump();
        Dash();
    }

    void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            anim.SetFloat("Speed_Multiplier", 2);
        }
        else if (doubleSpeed || doubleSpeed == false)
        {
            doubleSpeed = false;
            anim.SetFloat("Speed_Multiplier", 1);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !gameOver)
        {
            rb.AddForce(Vector3.up * speed,ForceMode.Impulse);
            isOnGround = false;
            anim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioSource.PlayOneShot(jumpSound,1f);
            isDoubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !isDoubleJumpUsed)
        {
            isDoubleJumpUsed = true;
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            anim.Play("Running_Jump", 3);
            audioSource.PlayOneShot(jumpSound, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstackle"))
        {
            gameOver = true;
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            Debug.Log("Game Over !!");
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound,1f);
        }
    }
}
