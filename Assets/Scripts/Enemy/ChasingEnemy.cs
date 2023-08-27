using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public float speed;
    public PlayerHealth player;
    public bool chase = false;
    public Transform startingPoint;
    private bool isFlip = false;

    void Update()
    {
        
        if(chase == true)
        {
            Chase();
        }
        else
        {
            ReturnStartPoint(); 
        }
        Flip();
    }

    private void Chase()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, 0);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFlip = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFlip = false;
        }
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position,startingPoint.position, speed * Time.deltaTime);
    }

}
