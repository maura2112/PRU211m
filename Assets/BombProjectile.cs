using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public float speed = 4;
    public Vector3 LaunchOffSet;
    public bool isThrown;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isThrown)
        {
            var diretion = -transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(diretion * speed, ForceMode2D.Impulse);
        }
        transform.Translate(LaunchOffSet);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isThrown)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var health = collision.collider.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(2);
            Destroy(gameObject);

        }
        Destroy(gameObject,1.5f);
    }
}
