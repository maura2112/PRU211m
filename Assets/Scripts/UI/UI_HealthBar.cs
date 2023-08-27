using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthBar : MonoBehaviour
{
    public GameObject heartPrefs;
    //public float health, maxHealth;
    public PlayerHealth playerHealth;
    List<UI_Health> hearts = new List<UI_Health>();

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
        PlayerHealth.OnPlayerHealth += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
        PlayerHealth.OnPlayerHealth -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.maxHealth % 4;
        int heartsToMake = (int)(playerHealth.maxHealth / 4 +maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++) { 
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 4), 0, 4);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }


    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefs);
        newHeart.transform.SetParent(transform);
        UI_Health heartComponent = newHeart.GetComponent<UI_Health>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach (Transform t in transform) {
        Destroy(t.gameObject);
        }
        hearts = new List<UI_Health> ();
    }
}

