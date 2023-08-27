using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Animator animator;
    private AudioSource pickItemSE;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pickItemSE = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pickItemSE.Play();
            animator.Play("Item-feedback-Animation");
            gameObject.GetComponent<Collider2D>().enabled = false;
            Invoke("OnDisableItem", 0.3f);
        }
    }

    private void OnDisableItem()
    {
        gameObject.SetActive(false);
    }


}
