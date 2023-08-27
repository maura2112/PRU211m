using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    //private Animator animator;
    //private AudioSource headEnemy;
    //private float bounce = 15f;
    EnemyDamage ed;

    private void Start()
    {
        //    animator = GetComponent<Animator>();
        //    headEnemy = GetComponent<AudioSource>();
        ed = GetComponent<EnemyDamage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision duoi chan player la trigger collider
        if (collision.tag == "Player")
        {
            ed.Die();
            
        }
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
