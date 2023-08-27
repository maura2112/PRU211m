using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        
        gameObject.SetActive(true);
        
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0 ,0 );
        lifetime += Time.deltaTime;
        if( lifetime > resetTime ) { 
        gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 2f) * bounce, ForceMode2D.Impulse);
            playerHealth.TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
