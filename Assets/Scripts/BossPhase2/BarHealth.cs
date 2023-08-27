using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHealth : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = hearts.Length - 1;
        foreach (var heart in hearts)
        {
            heart.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LifeUp()
    {
        if (currentIndex < hearts.Length - 1)
        {
            currentIndex++;
            hearts[currentIndex].SetActive(true);
        }
    }

    public void LifeDown()
    {
        if (currentIndex >= 0)
        {
            hearts[currentIndex].SetActive(false);
            currentIndex--;
        }
    }
}
