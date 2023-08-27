using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller_Ladder : MonoBehaviour
{
    private float vertical;
    private float speed = 5f;
    private Boolean isClimbing;
    public Boolean isLadder;
    private Animator animator;
    [SerializeField] private Rigidbody2D _rb;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
        vertical = Input.GetAxis("Vertical");
        

        //animator.SetFloat("Climb", Mathf.Abs(vertical));

        if (isLadder && Mathf.Abs(vertical) >= 0f)
        {
            animator.SetFloat("Climb", 0.2f);
            animator.SetFloat("Jump", 0);
            animator.SetFloat("Speed", 0);
            isClimbing = true;
        }
        if (!isLadder)
        {
            animator.SetFloat("Climb", 0f);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Keypad5))
        {
            vertical = -1;
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            vertical = 1;
        }
        if (Input.GetKeyUp(KeyCode.Keypad5) || Input.GetKeyUp(KeyCode.Keypad8))
        {
            vertical = 0;
        }
        if (isClimbing)
        {
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_rb.velocity.x, vertical*speed);
        }
        else
        {
            _rb.gravityScale = 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }


}
