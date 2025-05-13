using UnityEngine;

public class Pickup_x2 : MonoBehaviour
{
    //public bool activateDoubleShoot = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PickUpx2 Cogido");
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            shooting.doubleShoot = true; // ACTIVA doble disparo
            shooting.activarx2();

            Destroy(gameObject); // Destruye el pickup una vez recogido
        }
    }
}