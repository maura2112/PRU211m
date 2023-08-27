using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_Run : MonoBehaviour
{
    private Vector3 start;
    private float currentDistance;
    [SerializeField]private float speed =4f;
    [SerializeField]private float distance = 3f;
    private int reached;
    void Start()
    {
        start = transform.localPosition;
        reached = 1;
    }
    void Update()
    {
        Run();
    }
    private void Run()
    {
        transform.Translate(Vector3.right * reached * speed * Time.deltaTime);
        currentDistance += Mathf.Abs(speed * Time.deltaTime);
        if (currentDistance >= distance)
        {
            Flip();
            reached *= -1;
            currentDistance = 0f;
        }
    }
    void Flip()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
