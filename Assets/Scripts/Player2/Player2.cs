using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    //Player Behavior
    //

    private float maxYVelocity;
    //Run
    private float moveVertical;
    private float horizontal;
    private float speed = 9f;
    public bool isFacingRight = true;
    public bool isGrounded;
    [SerializeField] private ParticleSystem dust;

    //Jump
    public bool isJumping;
    [SerializeField] private float jumpingPower = 40f;
    private bool doubleJump;
    private CapsuleCollider2D capsuleCollider;
    //private Vector2 beforeCrouchSize;
    //private Vector2 beforeCrouchOffSet;


    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //Game Object priority
    private AudioSource jumpSE;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Player2Health playerHealth;
    private Animator animator;

    //Ground, Idle, Wall

    //Wind effect
    public Ball ball;

    //Respawn
    Transform endPos;
    Rigidbody2D rgbd;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        moveVertical = 0;
        horizontal = 0;
        animator = gameObject.GetComponent<Animator>();
        jumpSE = gameObject.GetComponent<AudioSource>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
        //beforeCrouchSize = capsuleCollider.size;
        //beforeCrouchOffSet = capsuleCollider.offset;


    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        Running();
        Crouching();
        Jumping();
        Dashing();
        Flip();

    }

    private void FixedUpdate()
    {

        if (isDashing)
        {
            return;
        }
        //Running();
        Crouching();
        Falling();
    }
    private void LateUpdate()
    {
        //if(rb.velocity.y > 0.1 || rb.velocity.y <-0.1)
        //{
        //    animator.SetTrigger("Jump");
        //}
        //else
        //{
        //    animator.ResetTrigger("Jump");
        //    animator.SetTrigger("Idle");
        //    animator.ResetTrigger("Idle");
        //}
    }

    private void Running()
    {
        rb.velocity = new Vector2(horizontal * speed , rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            horizontal = -1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            horizontal = 1;
        }
        if (Input.GetKeyUp(KeyCode.Keypad4) || Input.GetKeyUp(KeyCode.Keypad6))
        {
            horizontal = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        
    }

    private void Jumping()
    {
        if (IsGrounded())
        {
            animator.SetFloat("Jump", 0);
            transform.Find("ColliderRoll").gameObject.SetActive(false);
            if (!Input.GetKey(KeyCode.Keypad0))
            {
                doubleJump = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            maxYVelocity = 0;
            CreateDust();

            if (IsGrounded() || doubleJump)
            {
                jumpSE.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                doubleJump = !doubleJump;
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad0) && rb.velocity.y > 0f)
        {
            animator.SetFloat("Jump", 1);
            transform.Find("ColliderRoll").gameObject.SetActive(true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            moveVertical = -1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            moveVertical = 1;
        }
        if (Input.GetKeyUp(KeyCode.Keypad5) || Input.GetKeyUp(KeyCode.Keypad8))
        {
            moveVertical = 0;
        }

        if (moveVertical < 0.01f)
        {
            animator.SetFloat("Crouch", moveVertical);
        }
    }

    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPeriod) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Falling()
    {
        if (isGrounded)
        {
            if (maxYVelocity <= -18)
            {
                playerHealth.FallDamage();
                maxYVelocity = 0;
            }
        }
        else if (isJumping)
        {
            if (rb.velocity.y < maxYVelocity)
            {
                maxYVelocity = rb.velocity.y;
            }
        }

        if (moveVertical < 0.1f && !isJumping)
        {
            animator.SetFloat("Jump", 0);
            rb.AddForce(new Vector2(0f, moveVertical * jumpingPower * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }



    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            CreateDust();
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);



        tr.emitting = true;
        transform.Find("dash_Audio").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void CreateDust()
    {
        dust.Play();

    }

    public void Die()
    {

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "OneWayPlatform")
        {
            isJumping = false;
            isGrounded = true;
        }
        if (collision.gameObject.tag == "MovingPlat")
        {
            transform.parent = collision.transform;
            isJumping = false;
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            animator.Play("Hurt_Animation");
        }
        if (collision.gameObject.tag == "Gem")
        {
            MoneyManager.money += 1;
            ScoreManager.score += 200;
        }
        if (collision.gameObject.tag == "Cherry")
        {
            playerHealth.Healing(1);

            ScoreManager.score += 100;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "OneWayPlatform")
        {
            isJumping = true;
            isGrounded = false;
        }
        if (collision.gameObject.tag == "MovingPlat")
        {
            transform.parent = null;
            isJumping = true;
            isGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            animator.Play("Hurt_Animation");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
