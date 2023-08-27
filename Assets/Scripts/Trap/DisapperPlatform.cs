using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapperPlatform : MonoBehaviour
{
    [SerializeField] private float timeLast;
    [SerializeField] private float timeAppear;


    IEnumerator WaitingTime()
    {
        ShakeCamera.Instance.Shake(3f, timeLast);
        
        yield return new WaitForSeconds(timeLast);
        transform.Find("SpriteDisappear").gameObject.SetActive(false);
        Debug.Log("Platform is disappear!");
        Invoke(nameof(ActiveAgain), timeAppear);
        
        Debug.Log("Platform is appear again!");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (transform.Find("SpriteDisappear") != null)
            {
                    transform.Find("SpriteDisappear").gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(WaitingTime());

            }
            
        }  
    }

    private void ActiveAgain()
    {
        transform.Find("SpriteDisappear").gameObject.SetActive(true);
        transform.GetComponent<AudioSource>().Play();
    }


}
