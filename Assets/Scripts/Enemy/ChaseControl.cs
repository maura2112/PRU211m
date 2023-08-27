using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    public ChasingBeetle[] enemyArray;
    public ChasingEnemy[] enemyEnemyArray;
    public ChasingPet[] petArray;
    public FallingTrap[] trapArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach(ChasingBeetle enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            foreach (ChasingEnemy enemy1 in enemyEnemyArray)
            {
                enemy1.chase = true;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            foreach (ChasingPet pet in petArray)
            {
                pet.chase = true;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            foreach (FallingTrap trap in trapArray)
            {
                trap.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (ChasingBeetle enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            foreach (ChasingEnemy enemy1 in enemyEnemyArray)
            {
                enemy1.chase = false;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            foreach (ChasingPet pet in petArray)
            {
                pet.chase = false;
            }
        }
        
    }
}
