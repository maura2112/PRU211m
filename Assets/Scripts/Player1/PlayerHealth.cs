using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth ;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealth;

    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private PlayerController playerController;
    bool onGrounded;
    float maxYVelocity;
    private Rigidbody2D rb;
    [SerializeField] float beingFall;

    [SerializeField] private float fallBounce;

    string currentSceneName;
    private ScoreManager scoreManager;
    private int moneyWhenReset;
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        moneyWhenReset = PlayerPrefs.GetInt("TotalMoney");
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //audioSource = gameObject.GetComponent<AudioSource>()[1];
        audioSource = transform.Find("hurt_Audio").GetComponent<AudioSource>();
        health = maxHealth;
        rb = playerController.GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        
    }

    private void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void TakeDamage(float amount)
    {
        audioSource.Play();
        ShakeCamera.Instance.Shake(5f, 0.1f);
        playerController.GetComponent<Animator>().Play("Hurt_Animation");
        health -= amount;
        OnPlayerDamaged?.Invoke();
        if (health <= 0)
        {
            health = 0;
            animator.Play("Enemt-death-Animation");
            OnPlayerDeath?.Invoke();
            PlayerDie();
        }

    }

    public void Healing(float amount)
    {
        health += amount;
        OnPlayerHealth?.Invoke();
    }

    public void FallDamage()
    {
        playerController.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fallBounce, ForceMode2D.Impulse);
        TakeDamage(5);
    }

    public void PlayerDie()
    {
        StartCoroutine(PlayerRespawn(0.5f));
    }

    IEnumerator PlayerRespawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        ScoreManager.score = 0;
        PlayerPrefs.SetInt("TotalMoney", moneyWhenReset);
        SceneManager.LoadScene(currentSceneName);
    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }


}
