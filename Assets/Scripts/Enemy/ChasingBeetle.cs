using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBeetle : MonoBehaviour
{
    public float speed;
    public PlayerHealth player;

    public bool chase = false;
    public Transform startingPoint;
    private bool isFlip = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (chase == true)
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
        //Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, 0);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        //if (!isFlip)
        //{
        //    transform.Translate(Vector2.left * player.transform.position.x * speed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Translate(Vector2.right * player.transform.position.x * speed * Time.deltaTime);
        //}


        //if(Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        //{
        //    //change behavior(if)
        //}
        //else
        //{
        //    //reset variables
        //}

    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
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
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
}
