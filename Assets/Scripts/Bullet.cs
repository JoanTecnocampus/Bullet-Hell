using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 5f;
    public int damage = 1;
    public bool canBounce = false;
    public int maxBounces = 3;
    private int actualBounces = 0;

    public GameObject bulletExplosionPrefab;

    private Rigidbody2D rb;
    private bool isExploding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isExploding) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (!canBounce)
            {
                StartCoroutine(ExplodeAndDestroy());
                return;
            }
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            if (!canBounce)
            {
                StartCoroutine(ExplodeAndDestroy());
                return;
            }
        }

        actualBounces++;

        if (actualBounces > maxBounces)
        {
            StartCoroutine(ExplodeAndDestroy());
        }
    }

    IEnumerator ExplodeAndDestroy()
    {
        isExploding = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = false;
        }

        if (bulletExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);

            Animator anim = explosion.GetComponent<Animator>();
            if (anim != null)
            {
                float length = anim.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(length);
            }
        }

        Destroy(gameObject);
    }
}
