using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    private bool audioPlaying = false;

    void Update()
    {
        if (!isDone())
        {
            if (!audioPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
                audioPlaying = true;
            }
            //gameObject.GetComponent<AudioSource>().Play();
            ShakeCamera.Instance.Shake(1f, 0.1f);
            transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        }
        else
        {
            if (audioPlaying)
            {
                gameObject.GetComponent<AudioSource>().Stop();
                audioPlaying = false;
            }
            return;
        }

        


    }

    private bool isDone()
    {
        if (transform.position != endPoint.position)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
