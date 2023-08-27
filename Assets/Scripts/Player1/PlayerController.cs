using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player Behavior
    //

    private float maxYVelocity;
    //Run
    private float moveVertical;
    private float horizontal;
    private float speed = 8f;
    public bool isFacingRight = true;
    public bool isGrounded;
    [SerializeField] private ParticleSystem dust;

    //Jump
    public bool isJumping;
    [SerializeField] private float jumpingPower = 30f;
    private bool doubleJump;
    //public float hangTime = .2f;
    //private float hangCounter;
    //public float jumpBufferLength = .4f;
    //private float jumpBufferCount;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 beforeCrouchSize;
    private Vector2 beforeCrouchOffSet;


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
    [SerializeField] private PlayerHealth playerHealth;
    private Animator animator;

    //Ground, Idle, Wall
    public bool isWallSliding;
    //private  bool isWall = false;
    private float wallSlidingSpeed = 0.2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    public bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(20f, 40f);

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
        animator = gameObject.GetComponent<Animator>();
        jumpSE = gameObject.GetComponent<AudioSource>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
        beforeCrouchSize = capsuleCollider.size;
        beforeCrouchOffSet = capsuleCollider.offset;


    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        //Running();
        Jumping();
        Dashing();
        WallSlide();
        WallJump();
        if (!isWallJumping)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {

        if (isDashing)
        {
            return;
        }
        Running();
        Crouching();
        //Debug.DrawLine(transform.localPosition, transform.localPosition + new Vector3(rb.velocity.x, rb.velocity.y, 0));
        Falling();
    }

    private void Running()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jumping()
    {
        if (IsGrounded())
        {
            animator.SetFloat("Jump", 0);
            if (!Input.GetButton("Jump"))
            {
                doubleJump = false;
            }
        }

        //if (jumpBufferCount >= 0f && hangCounter > 0f)
        if (Input.GetButtonDown("Jump"))
        {
            maxYVelocity = 0;
            CreateDust();

            if (IsGrounded() || doubleJump)
            {
                jumpSE.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                //jumpBufferCount = 0;
                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            animator.SetFloat("Jump", 1f);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Crouching()
    {
        moveVertical = Input.GetAxisRaw("Vertical");

        if (moveVertical < 0.01f)
        {
            animator.SetFloat("Crouch", moveVertical);
            capsuleCollider.size = beforeCrouchSize;
            capsuleCollider.offset = beforeCrouchOffSet;
            
            if (moveVertical <0f)
            {
                Vector2 newSize = new Vector2(beforeCrouchSize.x, 0.1f);
                capsuleCollider.size = newSize;
                Vector2 newOffset = new Vector2(beforeCrouchOffSet.x, -0.34f);
                capsuleCollider.offset = newOffset;
            }
        }




    }

    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                //Vector3 localScale = transform.localScale;
                //localScale.x *= -1;
                //transform.localScale = localScale;
                transform.Rotate(0, 180, 0);

            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            CreateDust();
            isFacingRight = !isFacingRight;
            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
            transform.Rotate(0, 180, 0);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }

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
