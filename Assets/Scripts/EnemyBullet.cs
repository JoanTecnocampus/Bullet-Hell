using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;
    public int damage = 1;
    private int actualBounces = 0;

    public GameObject bulletExplosionPrefab;

    private Rigidbody2D rb;
    private bool isExploding = false;


    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }*/
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamagePlayer(damage);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            StartCoroutine(ExplodeAndDestroy());
        }

        IEnumerator ExplodeAndDestroy()
        {
            isExploding = true;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;

            if (bulletExplosionPrefab != null)
            {
                GameObject explosion = Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);

                Animator anim = explosion.GetComponent<Animator>();
                if (anim != null)
                {
                    float length = anim.GetCurrentAnimatorStateInfo(0).length;
                    yield return new WaitForSeconds(length);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }

            //Destroy(gameObject);
        }
    }
}
