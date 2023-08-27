using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTrap : MonoBehaviour
{
    public bool isNearButton;
    public bool isCollected;
    public bool isActive = false;

    private void FixedUpdate()
    {
        PressButton();
    }
    private void PressButton()
    {
        if (isNearButton)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("PressF");
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
    private IEnumerator TreasureDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.05f);
        obj.SetActive(true);
    }
}
