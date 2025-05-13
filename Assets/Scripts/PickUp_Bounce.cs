using UnityEngine;

public class PickUp_Bounce : MonoBehaviour
{
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.ActivarRebote(duration);
            }

            Destroy(gameObject);
        }
    }
}
