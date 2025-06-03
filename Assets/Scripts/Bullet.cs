using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 5f;
    public int damage = 1;
    public bool canBounce = false;
    public int maxBounces = 3;
    private int actualBounces = 0;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyTank") || collider.CompareTag("EnemySniper"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (!canBounce)
            {
                Destroy(gameObject);
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
                Destroy(gameObject);
                return;
            }
        }

        actualBounces++;

        if (actualBounces > maxBounces)
        {
            Destroy(gameObject);
            return;
        }

        else if (collider.CompareTag("Wall"))
        {
            if (!canBounce)
            {
                Destroy(gameObject);
            }
        }
    }
}