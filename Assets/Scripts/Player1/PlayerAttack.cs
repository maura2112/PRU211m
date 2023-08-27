using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerAttack : MonoBehaviour
{
    //near
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public PlayerController player;
    //far
    [SerializeField] private Transform firePoint;
    public GameObject bullet;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            NearAttack();
        }
        if (Input.GetKeyDown(KeyCode.K) )
        {

            FarAttack();

        }
    }

    private void NearAttack()
    {
        player.transform.Find("near-Attack").GetComponent<AudioSource>().Play();
        player.transform.Find("near-Attack").GetComponent<SpriteRenderer>().enabled = true;
        player.transform.Find("near-Attack").GetComponent<Animator>().enabled = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyDamage>().Die();
            }
        }



        Invoke("Attack_near_Delay", 0.2f);
    }
    private void Attack_near_Delay()
    {
        player.transform.Find("near-Attack").GetComponent<AudioSource>().Stop();
        player.transform.Find("near-Attack").GetComponent<SpriteRenderer>().enabled = false;
        player.transform.Find("near-Attack").GetComponent<Animator>().enabled = false;


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void FarAttack()
    {
        
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        //RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        //if (hit)
        //{
        //    Debug.Log(hit.transform.name);
        //    EnemyDamage enemy = hit.transform.GetComponent<EnemyDamage>();
        //    if(enemy != null)
        //    {
        //        enemy.Die();
        //    }
        //}


    }




}
