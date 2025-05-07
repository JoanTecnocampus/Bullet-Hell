using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool activateDoubleShoot = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PickUp Cogido");
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            shooting.doubleShoot = true; // ACTIVA doble disparo
            shooting.activarx2();

            Destroy(gameObject); // Destruye el pickup una vez recogido
        }
    }
}