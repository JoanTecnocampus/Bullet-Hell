using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthPlayer = 3;
    private int currentHealthPlayer;
    public Slider Slider;

    private SpriteRenderer spriteRenderer;
    private Coroutine damageFlashCoroutine;
    private Color originalColor;

    public bool shieldActive = false;
    
    public string sceneName;

    void Start()
    {
        // Inicializamos la salud actual como la salud máxima al comenzar el juego
        currentHealthPlayer = maxHealthPlayer;

        // Actualiza el slider al valor inicial
        Slider.maxValue = maxHealthPlayer;
        Slider.value = currentHealthPlayer;

        // Obtiene el sprite renderer si no está asignado
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamagePlayer(int damage)
    {
        if (shieldActive == false)
        {
            currentHealthPlayer -= damage;

            // Asegúrate de que la salud no se vuelva negativa
            if (currentHealthPlayer < 0)
                currentHealthPlayer = 0;

            // Actualiza el valor del slider cuando se reciba daño
            Slider.value = currentHealthPlayer;

            // Cancelar la corutina anterior si está corriendo
            if (damageFlashCoroutine != null)
            {
                StopCoroutine(damageFlashCoroutine);
            }

            damageFlashCoroutine = StartCoroutine(DamageFlash());

            if (currentHealthPlayer <= 0)
            {
                Die();
            }
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
        SceneManager.LoadScene(sceneName);
    }
    public void IncreaseMaxHealth(int amount)
    {
        maxHealthPlayer += amount;
        currentHealthPlayer += amount;

        if (currentHealthPlayer > maxHealthPlayer)
            currentHealthPlayer = maxHealthPlayer;

        Slider.maxValue = maxHealthPlayer;
        Slider.value = currentHealthPlayer;
    }


}