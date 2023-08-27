using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTreasure : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject interactors;
    public bool isNearButton ;
    //public bool isActiveInteractor = false;
    //public bool isIcon = false;
    public bool isCollected ;
    public bool isActive = false;
   


    private void Update()
    {

        
        if (isNearButton)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                Debug.Log("PressOpen");
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
                ActiveChildren();

            }
        }
        Deactive();

        if (isCollected)
        {
            gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter: " + collision.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearButton = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit: " + collision.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearButton = false;
        }
    }


    private void ActiveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            //StartCoroutine(TreasureDelay(child.gameObject));
            child.gameObject.SetActive(true);
        }
        isCollected = false;
        isActive = true;

    }
    private void Deactive()
    {
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {

            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf)
            {
                count++;
            }
        }
        if (count >= transform.childCount && isActive)
        {
            isCollected = true;
        }
    }

    private IEnumerator TreasureDelay(GameObject obj)
    {
        
        yield return new WaitForSeconds(0.05f);
        obj.SetActive(true);
    }
}

