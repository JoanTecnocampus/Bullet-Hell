using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageFlash()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;

            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = originalColor;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
