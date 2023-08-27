using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float m_jumpForce = 35f;

    private Rigidbody2D m_Rigidbody;
    private Manager m_Manager;

    // Gravity scale when jumping and falling
    [SerializeField] private float normalGravityScale = 10f;
    [SerializeField] private float fallingGravityScale = 40f;

    // Variable to determine if the game is over
    public bool gameOver = false;
    public bool isInvincible;

    // BarHealths
    [SerializeField] private GameObject[] BarHealths;

    // Lives System
    private int playerLives;
    private int maxLives;

    // Determine if the player is on ground
    private bool onGround;

    // Variables to control the number of jumps
    public int maxJumpCount = 2;
    public int jumpCount;
    public bool isInterating;

    // Sound Effects
    [SerializeField] AudioSource jumpingSFX;
    [SerializeField] AudioSource hurtingSFX;
    [SerializeField] AudioSource collectingSFX;
    [SerializeField] AudioSource winningSFX;



    // Particles
    public GameObject runParticle;
    public GameObject jumpParticle;
    public GameObject fallParticle;

    // Animator
    private Animator animator;

    void Start()
    {
        Time.timeScale = 1;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Manager = GameObject.Find("Game Manager").GetComponent<Manager>();
        animator = GetComponent<Animator>();

        onGround = true;
        jumpCount = 0;
        gameOver = false;
        isInterating = false;
        isInvincible = false;
        if (Scoring.instance != null)
        {
            maxLives = 6 - Scoring.instance.difficulty;
        }
        else
        {
            maxLives = 3;
        }
        playerLives = maxLives;

        runParticle.SetActive(true);
        jumpParticle.SetActive(false);
        fallParticle.SetActive(false);

        // Setup health bars
        foreach (GameObject BarHealth in BarHealths)
        {
            BarHealth.SetActive(false);
        }
        BarHealths[maxLives - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        // Handle jump
        if ((Input.GetButtonDown("Jump") ) && jumpCount < maxJumpCount && !gameOver && !isInterating)
        {
            // Reset the velocity before starting the next jump
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.AddForce(Vector2.up * (m_jumpForce - jumpCount * 5), ForceMode2D.Impulse);
            jumpCount++;
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
            jumpingSFX.Play();
            if (onGround)
            {
                jumpParticle.SetActive(true);
                runParticle.SetActive(false);
                onGround = false;
            }


        }

        // When jumping change to jump animation
        if (m_Rigidbody.velocity.y > 0)
        {
            m_Rigidbody.gravityScale = normalGravityScale;
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        // When falling falls faster for better feels 
        else if (m_Rigidbody.velocity.y < 0)
        {
            m_Rigidbody.gravityScale = fallingGravityScale;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);

        }
        // Change back to running animation
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // When hit the ground, reset jump
        if (collision.gameObject.CompareTag("Platform"))
        {
            fallParticle.SetActive(true);
            onGround = true;
            jumpCount = 0;
            runParticle.SetActive(true);
        }

        // If hit obstacle, died
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                playerLives--;
                BarHealths[maxLives - 1].GetComponent<BarHealth>().LifeDown();
                if (playerLives == 0)
                {
                    hurtingSFX.Play();
                    animator.SetBool("isDead", true);
                    gameOver = true;
                    Destroy(collision.gameObject);
                    Time.timeScale = 1;
                    StartCoroutine("TransitionToEndScene");
                }
                else
                {
                    hurtingSFX.Play();
                    animator.SetBool("isHurt", true);
                    Destroy(collision.gameObject);
                    isInvincible = true;
                    StartCoroutine("Invincible");
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectibles"))
        {
            collectingSFX.Play();
            m_Manager.AddScore(10);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Live-Up"))
        {
            collectingSFX.Play();
            if (playerLives < maxLives)
            {
                playerLives++;
                BarHealths[maxLives - 1].GetComponent<BarHealth>().LifeUp();
            }
            else
            {
                m_Manager.AddScore(10);
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Parrot"))
        {
            m_Manager.AddScore(500);
            winningSFX.Play();
            StartCoroutine("TransitionToWinScene");

        }

        if (other.CompareTag("Interactables"))
        {
            isInterating = true;
        }

        if (other.CompareTag("Boss"))
        {
            if (isInvincible)
            {

            }
            else
            {
                playerLives--;
                BarHealths[maxLives - 1].GetComponent<BarHealth>().LifeDown();
                if (playerLives == 0)
                {
                    hurtingSFX.Play();
                    animator.SetBool("isDead", true);
                    gameOver = true;
                    StartCoroutine("TransitionToEndScene");
                }
                else
                {
                    hurtingSFX.Play();
                    animator.SetBool("isHurt", true);
                    isInvincible = true;
                    StartCoroutine("Invincible");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactables"))
        {
            isInterating = false;
        }
    }

    IEnumerator TransitionToEndScene()
    {
        // Wait one second and load end scene
        yield return new WaitForSecondsRealtime(0.5f);
        m_Manager.SaveSccore();
        SceneManager.LoadScene(2);
    }

    IEnumerator TransitionToWinScene()
    {
        // Wait one second and load end scene
        yield return new WaitForSecondsRealtime(0.3f);
        gameOver = true;
        m_Manager.SaveSccore();
        SceneManager.LoadScene(3);
    }

    IEnumerator Invincible()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        animator.SetBool("isHurt", false);
        isInvincible = false;
    }
}
