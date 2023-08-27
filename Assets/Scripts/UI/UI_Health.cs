using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    //public Image[] hearts;
    public Sprite fullHeart, quarterPastHeart, halfHeart, quarterToHeart, emptyHeart;
    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)

    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.quarterTo:
                heartImage.sprite = quarterToHeart;
                break;
            case HeartStatus.half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.quaterPast:
                heartImage.sprite = quarterPastHeart;
                break;
            case HeartStatus.full:
                heartImage.sprite = fullHeart;
                break;
        }
    }




}

public enum HeartStatus
{
    Empty = 0,
    quarterTo = 1,
    half = 2,
    quaterPast = 3,
    full = 4

}
