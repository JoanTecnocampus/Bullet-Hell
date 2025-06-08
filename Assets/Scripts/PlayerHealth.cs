using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthPlayer = 3;
    public int currentHealthPlayer;
    public Slider Slider;

    private SpriteRenderer spriteRenderer;
    private Coroutine damageFlashCoroutine;
    private Color originalColor;

    public bool shieldActive = false;
    
    public string sceneName;

    public GameObject explosionPrefab;
    
    public AudioSource[] AudioPlayerExplosion;
    
    public float delayDestroyFloat;
    
    private bool bIsDead = false;

    void Awake()
    {
        AudioPlayerExplosion = GetComponents<AudioSource>();
    }
    
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
        if (bIsDead) return;
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
        // Añadir Explosión
        if (explosionPrefab != null)
        {
            if (AudioPlayerExplosion.Length > 1)
            {
                AudioPlayerExplosion[1].Play(); // El segundo AudioSource (Explosion)
            }
            StartCoroutine(DelayDestroy(delayDestroyFloat));
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        bIsDead = true;
        //Destroy(gameObject);
        
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
    
    private IEnumerator DelayDestroy(float delay)
    {
        // Desde otro script en el mismo GameObject:
        PlayerController script = GetComponent<PlayerController>();
        if (script != null)
        {
            script.enabled = false;
        }
        PlayerShooting script2 = GetComponent<PlayerShooting>();
        if (script2 != null)
        {
            script2.enabled = false;
        }
        CameraFollow script3 = GetComponent<CameraFollow>();
        if (script3 != null)
        {
            script3.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        Destroy(gameObject);
    }
}