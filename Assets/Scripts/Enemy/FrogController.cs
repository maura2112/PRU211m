using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float jumpTime = 1.0f;
    public float jumpHeight = -1;
    public float groundLevel = -2.66f;
    private float jumpSpeed = 4.98f;
    public bool isJump;
    public bool isWait;

    // Start is called before the first frame update
    void Start()
    {
        isJump = true;
        isWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJump && !isWait)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
            if (transform.position.y >= jumpHeight)
            {
                isJump = false;
            }
            transform.Translate(Vector3.left * Time.deltaTime * 2);
        }
        if (!isJump && !isWait)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 2);
            transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);
            if (transform.position.y <= groundLevel)
            {
                isJump = true;
                isWait = true;
                StartCoroutine("Wait");
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        isWait = false;
    }
}
