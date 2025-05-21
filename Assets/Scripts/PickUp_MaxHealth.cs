using UnityEngine;

public class Pickup_MaxHealth : MonoBehaviour
{
    public int extraHealth = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseMaxHealth(extraHealth);
                Debug.Log("Vida máxima aumentada");
            }

            Destroy(gameObject);
        }
    }
}
