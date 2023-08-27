using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] public float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private Collider2D circleCollider2D;
    private Animator animator;
    private PlayerController playerController;



    
    private void Awake()
    {
        circleCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 2)
        {
            gameObject.SetActive(false);
        }


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        EnemyDamage enemy = collision.GetComponent<EnemyDamage>();
        if (enemy != null)
        {
            enemy.GetComponent<AudioSource>().Play();
            enemy.GetComponent<Animator>().Play("Enemt-death-Animation");

            StartCoroutine(DisableCollisionWithDelay(enemy.gameObject));
            //enemy.gameObject.SetActive(false);
        }

        
        //animator.SetTrigger("stop");

    }

    private IEnumerator DisableCollisionWithDelay(GameObject obj)
    {
        
        Debug.Log("Enemy Die");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f); 
        obj.SetActive(false);
        Deactive();

    }







    private void OnCollisionEnter2D(Collision2D collision)
    {

        animator.SetTrigger("stop");
    }



    public void SetDirection(float _direction)
    {
        playerController = FindObjectOfType<PlayerController>();
        lifetime = 0;
        if (playerController.isFacingRight)
        {
            direction = _direction;
        }
        else
        {
            
            direction = -_direction;
        }

        gameObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        hit = false;
        circleCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;



        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        Invoke("Deactive", 2f);

    }

    private void Deactive()
    {
        //
        gameObject.SetActive(false);
    }




}
