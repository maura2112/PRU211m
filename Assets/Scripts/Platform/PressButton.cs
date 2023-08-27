using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject interactors;
    public PlayerController player;
    public Player2 player2;
    public bool isNearButton = false;
    public bool isActiveInteractor = false;
    public bool isIcon = false;
    private void Update()
    {
        if (haveHead())
        {
            player.transform.Find("!Head").GetComponent<SpriteRenderer>().enabled = isNearButton;
        }

        if (isNearButton)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                isActiveInteractor = !isActiveInteractor;
                Debug.Log("PressF");
                gameObject.GetComponent<AudioSource>().Play();
                interactors.GetComponent<MonoBehaviour>().enabled = isActiveInteractor;
            }
        }
    }

    public bool haveHead()
    {
        if (player.transform.Find("!Head").GetComponent<SpriteRenderer>() != null)
        {
            return true;
        }
        else
        {
            return false;
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



}
