using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    public bool isTouched = false;
    public bool isInZone;


    void Update()
    {
        if( transform.position == endPoint.position)
        {
            isTouched = true;
        }
        if (isTouched )
        {   
            if(isInZone) {
                ShakeCamera.Instance.Shake(1f, 0.05f);
            }
            transform.position = Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
            if(transform.position == startPoint.position )
            {
                Invoke(nameof(Delay), 0.5f);
            }
        }
        
        if (!isTouched || transform.position == startPoint.position) 
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            
        }




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isInZone)
            {
                ShakeCamera.Instance.Shake(3f, 0.5f);
            }
            isTouched = true;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (isInZone)
            {
                ShakeCamera.Instance.Shake(3f, 0.3f);
            }
            Invoke(nameof(Wait), 1f);
        }
        
    }

    void Delay()
    {
        isTouched = false;
    }
    void Wait()
    {
        isTouched = true;
    }

}
