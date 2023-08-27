using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFallingTrap : MonoBehaviour
{
    public float speed;
    public PlayerHealth player;

    public Transform startingPoint;
    public Transform endingPoint;
    private bool isDrop;

    void Update()
    {
        Drop();
        Dropped();
    }
    private void Drop()
    {
        transform.position = Vector2.MoveTowards(transform.position, endingPoint.position, speed * Time.deltaTime);
    }
    private void Dropped()
    {
        if (!isDrop && transform.position == endingPoint.position)
        {
            isDrop = true;
            gameObject.GetComponent<AudioSource>().Play();
            ShakeCamera.Instance.Shake(5f, 0.3f);
        }
    }
}
