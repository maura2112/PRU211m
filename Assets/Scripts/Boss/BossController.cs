
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace StatePattern
{
    public class BossController : EnemyBoss
    {
        public GameObject playerObj;
        public int maxHealth = 100;
        public int currentHealth;
        public BossHealth healthBar;

        EnemyFSM bossMode = EnemyFSM.Idle;
        void Start()
        {

            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TakeDmg(7);
            }
            UpdateEnemy(playerObj.transform);
        }

        public void TakeDmg(int amount)
        {
            currentHealth -= amount;
            healthBar.SetHealth(currentHealth);
        }

        public override void UpdateEnemy(Transform playerObj)
        {
            //The distance between the Creeper and the player
            //float distance = (base.enemyObj.position - playerObj.position).magnitude;

            switch (bossMode)
            {
                case EnemyFSM.Idle:
                    if (currentHealth <= 98f && currentHealth >80f)
                    {
                        bossMode = EnemyFSM.Shooting;
                    }
                    break;
                case EnemyFSM.Shooting:
                    if (currentHealth <= 80f && currentHealth > 60f)
                    {
                        bossMode = EnemyFSM.Throw;
                    }
                    break;
                case EnemyFSM.Throw:
                    if (currentHealth <= 60f && currentHealth >40f)
                    {
                        bossMode = EnemyFSM.Dive;
                    }
                    break;
                case EnemyFSM.Dive:
                    if (currentHealth <= 40f && currentHealth > 20f)
                    {
                        bossMode = EnemyFSM.Spawn;
                    }
                    break;
                case EnemyFSM.Spawn:
                    if (currentHealth <= 20f && currentHealth >0f)
                    {
                        bossMode = EnemyFSM.Shield;
                    }
                    break;
                case EnemyFSM.Shield:
                    if (currentHealth <= 0)
                    {
                        Die();
                    }
                    
                    break;
                

            }
            

            //Move the enemy based on a state
            DoAction(playerObj, bossMode, GetAnimator());
        }
    }
}
