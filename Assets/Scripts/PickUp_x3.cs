using UnityEngine;

public class Pickup_x3 : MonoBehaviour
{
    //public bool activateTripleShoot = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PickUpx3 Cogido");
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            shooting.tripleShoot = true; // ACTIVA triple disparo
            shooting.activarx3();

            Destroy(gameObject); // Destruye el pickup una vez recogido
        }
    }
}