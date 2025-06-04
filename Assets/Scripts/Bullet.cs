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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isExploding) return;

        if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyTank") || collider.CompareTag("EnemySniper"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (!canBounce)
            {
                StartCoroutine(ExplodeAndDestroy());
                return;
            }
            else
            {
                rb.linearVelocity *= -1;
            }
        }
        else if (collider.CompareTag("Wall"))
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
            return;
        }
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

        Destroy(gameObject);
    }
}
