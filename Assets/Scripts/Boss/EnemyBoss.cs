using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class EnemyBoss : MonoBehaviour
    {
        //Sprite
        public PlayerHealth playerHealth;
        [SerializeField] protected float damage;
        protected float bounce = 10f;
        public Animator animator;
        public AudioSource audio;
        private EnemyDamage bossDamage;
        private bool isDefeated;
        public AudioSource state2;
        public AudioSource state3;
        public AudioSource state4;
        public AudioSource state5;
        private bool isState5;

        //Shooting
        [SerializeField] private Transform firePoint;
        public GameObject bullet;
        private bool isShooting;

        //Throwning
        [SerializeField] private Transform thrownPoint;
        public GameObject bomb;
        private bool isThrowning;

        //Diving
        public Transform startPoint;
        public Transform flyingPoint;
        public Transform divingPoint;
        public float flySpeed;
        public float diveSpeed;
        private bool isDive;
        private bool isFlying = true;
        private bool isDiving;
        private bool isFlyingBack;

        //Spawning
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private float spawnTime; // random
        public Transform spawnPoint;
        private bool isSpawning;

        



        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            audio = gameObject.GetComponent<AudioSource>();
        }
        private void LateUpdate()
        {
            if (isDive && isFlying)
            {
                Flying();
            }
            else if (isDive && isDiving)
            {
                Diving();
            }
            else if (isDive && isFlyingBack)
            {
                FlyingBack();
            }

                


        }
        #region Enum
        protected enum EnemyFSM
        {
            Idle,
            Shooting,
            Throw,
            Dive,
            Spawn,
            Shield
        }

        public virtual void UpdateEnemy(Transform playerObj)
        {

        }

        protected Animator GetAnimator()
        {
            return animator;
        }
        #endregion

        #region Boss's State
        protected void DoAction(Transform playerObj, EnemyFSM enemyMode, Animator animator)
        {

            switch (enemyMode)
            {
                case EnemyFSM.Idle:
                    Debug.Log("Idle");
                    Debug.Log("State 0");
                    break;
                case EnemyFSM.Shooting:
                    if (!isShooting)
                    {
                        Debug.Log("Change State 1");
                        isShooting = true;
                        StartCoroutine(ShootingDelay(Shooting));
                    }
                    break;
                case EnemyFSM.Throw:
                    if (!isThrowning)
                    {
                        animator.SetTrigger("Hurt");
                        Debug.Log("Change State 2");
                        isThrowning = true;
                        StartCoroutine(ThrowingDelay(Throwning));
                    }
                    break;
                case EnemyFSM.Dive:
                    if (!isDive)
                    {
                        isDive = true;
                    }
                    break;
                case EnemyFSM.Spawn:
                    if (!isSpawning)
                    {
                        animator.SetTrigger("Hurt");
                        Debug.Log("Change State 4");
                        isSpawning = true;
                        StartCoroutine(SpawningDelay(Spawning));
                    }
                    //animator.SetTrigger("Hurt");
                    break;
                case EnemyFSM.Shield:
                    if (!isState5)
                    {
                        state5.Play();
                        isState5 = true;
                    }
                    if (!isThrowning)
                    {
                        animator.SetTrigger("Hurt");
                        Debug.Log("Change State 5");
                        isThrowning = true;
                        StartCoroutine(ThrowingDelay(Throwning));
                    }
                    Shielding();
                    break;
            }

        }
        #endregion

        #region Boss Die and Take Damage
        public void Die()
        {
           
            if (!isDefeated)
            {
                ScoreManager.score += 5000;
                audio.Play();
                animator.Play("Enemt-death-Animation");
                gameObject.GetComponent<Collider2D>().enabled = false;
                Invoke("Defeat", 2f);
                isDefeated = true;
            }
        }

        private void Defeat()
        {
            gameObject.SetActive(false);
            ScenceController.instance.NextLevel();
            Debug.Log("Defeat");

        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 2f) * bounce, ForceMode2D.Impulse);
                playerHealth.TakeDamage(damage);
            }

        }
        #endregion

        #region Boss's Action
        //Shooting
        public void Shooting()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);


        }
        private IEnumerator ShootingDelay(System.Action functionToCall)
        {
            while (isShooting)
            {
                float randomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(randomTime);
                functionToCall.Invoke();
                isShooting = false;
            }
        }

        //Throwning
        public void Throwning()
        {
            state2.Play();
            Instantiate(bomb, thrownPoint.position, thrownPoint.rotation);
        }
        private IEnumerator ThrowingDelay(System.Action functionToCall)
        {
            while (isThrowning)
            {

                float randomTime = Random.Range(0.75f, 1.5f);
                yield return new WaitForSeconds(randomTime);
                functionToCall.Invoke();
                isThrowning = false;

            }
        }

        //Diving
        public void Flying()
        {
            transform.position = Vector2.MoveTowards(transform.position, flyingPoint.position, flySpeed * Time.deltaTime);
            animator.Play("Eagle-Animation");
            Debug.Log("Flying");
            state3.Play();
            if (transform.position == flyingPoint.position)
            {
                isDiving = true;
                isFlying = false;
                Diving();
            }
        }
        public void Diving()
        {
            ShakeCamera.Instance.Shake(5f, 0.1f);
            animator.Play("Eagle-dive-attack-Animation");
            transform.position = Vector2.MoveTowards(transform.position, divingPoint.position, diveSpeed * Time.deltaTime);
            //animator.Play("Eagle-dive-attack-Animation");
            Debug.Log("Diving");
            if (transform.position == divingPoint.position)
            {
                isFlyingBack = true;
                isDiving = false;
                FlyingBack();
            }

        }
        public void FlyingBack()
        {
            transform.position = Vector2.MoveTowards(transform.position, startPoint.position, flySpeed * 2 * Time.deltaTime);
            animator.Play("Eagle-Animation");
            Debug.Log("Flying Back");
            if (transform.position == startPoint.position)
            {
                Debug.Log("Done");
                isFlying = true;
                isFlyingBack = false;
                isDive = false;
            }
        }

        //Spawning
        private void Spawning()
        {
            state4.Play();
            enemyPrefab[FindEnemyPref()].transform.position = spawnPoint.position;
            enemyPrefab[FindEnemyPref()].GetComponent<SpawningEnemy>().ActivateProjectile();
        }
        private int FindEnemyPref()
        {
            int pos = Random.Range(0, enemyPrefab.Length);
            if (!enemyPrefab[pos].activeInHierarchy)
            {
                return pos;
            }
            return 0;
        }
        private IEnumerator SpawningDelay(System.Action functionToCall)
        {
            while (isSpawning)
            {
                yield return new WaitForSeconds(1);
                functionToCall.Invoke();
                isSpawning = false;

            }
        }

        //Shielding
        private void Shielding()
        {
            transform.Find("ShieldPoint").gameObject.SetActive(true);
        }
        #endregion


    }
}
