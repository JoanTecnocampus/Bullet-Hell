//Codigo Mio
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
    public float stopDistance = 2.0f; // Distancia a la que se detiene el enemigo si no hay slot

    private Transform player;
    private Transform targetSlot;

    public int randomGeneratePowerUp;
    public int randomPowerUp;

    public GameObject x2PickUp;
    public GameObject x3PickUp;
    public GameObject shieldPickUp;
    public GameObject bouncyPickUp;
    public GameObject moreHealthPickUp;

    private bool bIsDead = false;

    public void AssignSlotExternally(Transform newSlot)
    {
        targetSlot = newSlot;
    }
    
    void Start()
    {
        if (EnemyCounter.instance != null)
        {
            EnemyCounter.instance.AddEnemy(); // ✅ Contador sube al generarse el enemigo
        }

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

        /*// Buscar slot disponible según el tag del enemigo
        targetSlot = EnemySlotManager.Instance.GetFreeSlot(gameObject.tag);
        if (targetSlot != null)
        {
            EnemySlotManager.Instance.AssignSlot(targetSlot, gameObject);
        }*/
        
        targetSlot = EnemySlotManager.Instance.GetFreeSlot(gameObject.tag);
        if (targetSlot != null)
        {
            EnemySlotManager.Instance.AssignSlot(targetSlot, gameObject);
        }
        else
        {
            // No hay slot, se registra como enemigo en espera
            EnemySlotManager.Instance.RegisterWaitingEnemy(this);
        }

    }

    void Update()
    {
        if (targetSlot != null)
        {
            // Ir al slot si está asignado
            float distance = Vector2.Distance(transform.position, targetSlot.position);
            if (distance > 0.1f)
            {
                Vector2 direction = (targetSlot.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

                // (Opcional) Rotar al enemigo hacia el slot
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        else if (player != null)
        {
            // Si no hay slot disponible, acercarse al jugador pero detenerse a cierta distancia
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (bIsDead) return;

        currentHealthEnemy -= damage;

        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }

        damageFlashCoroutine = StartCoroutine(DamageFlash());

        if (currentHealthEnemy <= 0)
        {
            generateProbability();
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

    void generateProbability()
    {
        // Random del 1 al 10 para decidir si se genera el PowerUp
        randomGeneratePowerUp = Random.Range(1, 11);
        Debug.Log("Número aleatorio probabilidad generar PowerUp: " + randomGeneratePowerUp);

        switch (randomGeneratePowerUp)
        {
            case 6:
                Debug.Log("¡Se activó el evento especial del 6!");
                // Aquí va tu lógica especial, por ejemplo:
                generatePowerUp();
                break;
            default:
                Debug.Log("No pasó nada especial.");
                break;
        }
    }

    void generatePowerUp()
    {
        // Random del 1 al 5 para decidir cual PowerUp se genera
        randomPowerUp = Random.Range(1, 6);
        Debug.Log("Número aleatorio Generar PowerUp: " + randomPowerUp);

        switch (randomPowerUp)
        {
            case 1:
                Debug.Log("Salió el 1");
                generatePickup(x2PickUp);
                break;
            case 2:
                Debug.Log("Salió el 2");
                generatePickup(x3PickUp);
                break;
            case 3:
                Debug.Log("Salió el 3");
                generatePickup(shieldPickUp);
                break;
            case 4:
                Debug.Log("Salió el 4");
                generatePickup(bouncyPickUp);
                break;
            case 5:
                Debug.Log("Salió el 5");
                generatePickup(moreHealthPickUp);
                break;
            default:
                Debug.Log("Número fuera de rango");
                break;
        }
    }

    void generatePickup(GameObject pickUp)
    {
        if (pickUp != null)
        {
            Instantiate(pickUp, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No se asignó un prefab al spawner.");
        }
    }

    void Die()
    {
        if (EnemyCounter.instance != null)
        {
            EnemyCounter.instance.RemoveEnemy(); // ✅ Contador baja al morir el enemigo
        }

        if (targetSlot != null && EnemySlotManager.Instance != null)
        {
            EnemySlotManager.Instance.ReleaseSlot(targetSlot); // ✅ Liberar el slot
        }

        bIsDead = true;
        Destroy(gameObject);
    }
}
