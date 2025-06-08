using System.Collections;
using UnityEngine;

public class Pickup_MaxHealth : MonoBehaviour
{
    public int extraHealth = 25;
    
    public AudioSource AudioPickUpMaxHealth;
    //private AudioSource AudioEnemyExplosion;

    public float delayDestroyFloat;
    
    void Awake()
    {
        AudioPickUpMaxHealth = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseMaxHealth(extraHealth);
                Debug.Log("Vida máxima aumentada");
                AudioPickUpMaxHealth.Play();
                StartCoroutine(DelayDestroy(delayDestroyFloat));
            }

            //Destroy(gameObject);
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
