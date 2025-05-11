using UnityEngine;

public class PickUp_shield : MonoBehaviour
{
    public float shieldDuration = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShield shield = collision.GetComponent<PlayerShield>();
            if (shield != null)
            {
                shield.ActivateShield(shieldDuration);
            }

            Destroy(gameObject);
        }
    }
}
