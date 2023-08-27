using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemy : EnemyDamage
{
    public Transform targetPoint;
    public float speed;
    public Transform startPoint;

    private void Start()
    {
        base.isSpawn = true;

    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        
    }
    public void ActivateProjectile()
    {
        gameObject.transform.position = startPoint.position;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
    }
}
