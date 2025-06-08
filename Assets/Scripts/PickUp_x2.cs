using UnityEngine;
using System.Collections;

public class Pickup_x2 : MonoBehaviour
{
    //public bool activateDoubleShoot = false;
    public AudioSource AudioPickUpx2Shoot;
    //private AudioSource AudioEnemyExplosion;

    public float delayDestroyFloat;
    
    void Awake()
    {
        AudioPickUpx2Shoot = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PickUpx2 Cogido");
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            shooting.doubleShoot = true; // ACTIVA doble disparo
            shooting.activarx2();
            AudioPickUpx2Shoot.Play();
            StartCoroutine(DelayDestroy(delayDestroyFloat));

            //Destroy(gameObject); // Destruye el pickup una vez recogido
        }
    }
    
    private IEnumerator DelayDestroy(float delay)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<Rigidbody2D>().Sleep();
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}