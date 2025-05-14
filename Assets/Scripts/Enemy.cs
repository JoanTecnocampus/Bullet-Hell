using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealthEnemy = 3;
    private int currentHealthEnemy;

    private SpriteRenderer spriteRenderer;
    private Coroutine damageFlashCoroutine;
    private Color originalColor;

    public float moveSpeed = 2.0f;
    public float stopDistance = 2.0f; // Distancia a la que se detiene el enemigo

    private Transform player;

    void Start()
    {
        currentHealthEnemy = maxHealthEnemy;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

                // (Opcional) Rotar al enemigo hacia el jugador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealthEnemy -= damage;

        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }

        damageFlashCoroutine = StartCoroutine(DamageFlash());

        if (currentHealthEnemy <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
        damageFlashCoroutine = null;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
