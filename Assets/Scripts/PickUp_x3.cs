using UnityEngine;
using System.Collections;

public class Pickup_x3 : MonoBehaviour
{
    //public bool activateTripleShoot = false;
    public AudioSource AudioPickUpx3Shoot;
    //private AudioSource AudioEnemyExplosion;

    public float delayDestroyFloat;
    
    void Awake()
    {
        AudioPickUpx3Shoot = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PickUpx3 Cogido");
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            shooting.tripleShoot = true; // ACTIVA triple disparo
            shooting.activarx3();
            AudioPickUpx3Shoot.Play();
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