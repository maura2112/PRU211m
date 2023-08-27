using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingPet : MonoBehaviour
{
    public float speed;
    public PlayerHealth player;

    public bool chase = false;
    public Transform startingPoint;
    private bool isFlip = false;
    private AudioSource audio;
    private bool audioPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            if (!audioPlaying)
            {
                audio.Play();
                audioPlaying = true;
            }
            gameObject.GetComponent<Animator>().Play("Dog-Animation");
            Chase();
        }
        else
        {
            if (audioPlaying)
            {
                audio.Stop();
                audioPlaying = false;
            }
            gameObject.GetComponent<Animator>().Play("Dog-Idle-Animation");
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
    //private void StopDogSound()
    //{
    //    gameObject.GetComponent<AudioSource>().Stop();
    //}
    //IEnumerator StopDogSound()
    //{
    //    audio.Play();
    //    yield return 0.2f;
    //    audio.Stop();
    //}
}
