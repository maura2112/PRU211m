using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    // Attributes of the Boss
    private int lives = 4;
    private int liveUpCountdown;

    // State boolean
    public bool isIdle;
    public bool isEnrage;

    private float startDelay = 4;
    private float repeatRate = 6;

    private Animator bossAnimator;
    private PlayerManager playerController;
    private Manager Manager;

    // Obstacles to Spawn
    public GameObject soldierPrefab;
    public GameObject bombPrefab;
    public GameObject liveUpPrefab;
    public GameObject beerPrefab;


    // Interactables to Spawn
    public GameObject cannonPrefab;
    public GameObject bouncePadPrefab;
    private Vector2 bouncePadSpawnPos = new Vector2(4.8f, -2.5f);

    // Sound Effects;
    public AudioSource bossFallSFX;
    public AudioSource bossHurtSFX;
    public AudioSource bossEnrageSFX;
    public AudioSource bossPreparationSFX;
    public AudioSource bossChargeSFX;
    public AudioSource bossSpawnObstacleSFX;
    public AudioSource bossSummonSFX;
    public AudioSource bossDeathSFX;


    // Start is called before the first frame update.
    void Start()
    {
        liveUpCountdown = 0;
        isEnrage = false;
        isIdle = false;
        bossAnimator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerManager>();
        Manager = GameObject.Find("Game Manager").GetComponent<Manager>();
        InvokeRepeating("AttackRoutine", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cannonball"))
        {
            bossHurtSFX.Play();
            switch (lives)
            {
                case 4:
                    isIdle = false;
                    bossAnimator.SetTrigger("isHit");
                    lives--;
                    break;
                case 3:
                    isIdle = false;
                    bossAnimator.SetTrigger("isDeadHit");
                    lives--;
                    StartCoroutine(BossEnrage());
                    isEnrage = true;
                    break;
                case 2:
                    isIdle = false;
                    bossAnimator.SetTrigger("isHit");
                    lives--;
                    break;
                case 1:
                    isIdle = false;
                    bossAnimator.SetTrigger("isDeadHit");
                    lives--;
                    isEnrage = false;
                    Manager.isWon = true;
                    break;
            }
        }
    }
    void AttackRoutine()
    {
        if (!playerController.gameOver && isIdle && !Manager.isWon)
        {
            isIdle = false;
            int index = Random.Range(0, 10);
            // 40%: Charge Attack
            if (index < 4)
            {
                bossPreparationSFX.Play();
                bossAnimator.SetTrigger("Preparation");
                StartCoroutine(ChargePreparation());
                Instantiate(bouncePadPrefab, bouncePadSpawnPos, bouncePadPrefab.transform.rotation);

            }
            // 30% Spawn Obstacles
            else if (index >= 4 && index < 7)
            {
                bossSpawnObstacleSFX.Play();
                bossAnimator.SetTrigger("SpawnObstacle");
                int numOfWaves = 4;
                float spawnPosX = 5;
                for (int i = 0; i < numOfWaves; i++)
                {
                    spawnPosX += 5;
                    int rand = Random.Range(0, 2);
                    for (int j = 0; j < 3; j++)
                    {
                        float spawnPosY = j * 2 - 2;
                        if (j != rand * 2)
                        {
                            Instantiate(bombPrefab, new Vector3(spawnPosX, spawnPosY, 0), bombPrefab.transform.rotation);
                        }
                        else if (i == numOfWaves - 1)
                        {
                            if (liveUpCountdown > 2)
                            {
                                Instantiate(liveUpPrefab, new Vector3(spawnPosX, spawnPosY, 0), liveUpPrefab.transform.rotation);
                                liveUpCountdown = 0;
                            }
                            else
                            {
                                liveUpCountdown++;
                            }

                        }
                        else
                        {
                            if ((int)Random.Range(0, 4) > 2)
                            {
                                Instantiate(beerPrefab, new Vector3(spawnPosX, spawnPosY, 0), beerPrefab.transform.rotation);
                            }
                        }
                    }
                }
            }
            else // 30%: Summon Soldiers
            {
                bossSummonSFX.Play();
                bossAnimator.SetTrigger("Summon");
                float spawnPosX = 10;
                int numOfSpawns = 10;
                Instantiate(cannonPrefab, new Vector3(spawnPosX, -2.5f, 0), cannonPrefab.transform.rotation);
                for (int i = 0; i < numOfSpawns; i++)
                {
                    spawnPosX = 10 + 9 + (i * 0.5f);
                    Instantiate(soldierPrefab, new Vector3(spawnPosX, -2, 0), soldierPrefab.transform.rotation);
                }
            }
        }
    }

    IEnumerator ChargePreparation()
    {
        yield return new WaitForSeconds(1.5f);
        bossAnimator.SetTrigger("Charge");
        bossChargeSFX.Play();
    }

    IEnumerator ReturnToIdle(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        bossAnimator.SetTrigger("isIdle");
        isIdle = true;
    }

    IEnumerator BossEnrage()
    {
        yield return new WaitForSeconds(1);
        bossEnrageSFX.Play();
    }
}
