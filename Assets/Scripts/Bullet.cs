using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    void Explode()
    {
        if (bulletExplosionPrefab != null)
        {
            Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (!canBounce)
            {
                Explode();
                Destroy(gameObject);
            }
        }

        else if (collision.collider.CompareTag("Wall"))
        {
            if (!canBounce)
            {
                Explode();
                Destroy(gameObject);
                return;
            }
        }

        actualBounces++;

        if (actualBounces > maxBounces)
        {
            Explode();
            Destroy(gameObject);
        }
    }
}
