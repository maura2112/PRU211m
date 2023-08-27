using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    public static int money;

    private void Start()
    {
            money = PlayerPrefs.GetInt("TotalMoney");
    }
    void Update()
    {
        PlayerPrefs.SetInt("TotalMoney", money);
        moneyText.text = "" + Mathf.Round(money);
    }

    public void AddScore(int amount)
    {
        money += amount;
    }
}
