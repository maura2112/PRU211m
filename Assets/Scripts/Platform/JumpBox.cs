using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpBox : MonoBehaviour
{
    [SerializeField] private float bouncePower;
    [SerializeField] private float waitTime;
    public bool isJumped;

    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumped = false;
                StartCoroutine(Boucing(collision.gameObject));
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumped = true;
        }
    }

    IEnumerator Boucing(GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);
        if (!isJumped)
        {
            gameObject.GetComponent<AudioSource>().Play();
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
            isJumped = true;
            Debug.Log(obj.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            yield return null;
        }


    }
}
