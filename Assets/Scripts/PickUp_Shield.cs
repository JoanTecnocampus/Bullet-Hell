using System.Collections;
using UnityEngine;

public class PickUp_shield : MonoBehaviour
{
    public float shieldDuration = 20f;
    
    public AudioSource AudioPickUpShield;
    //private AudioSource AudioEnemyExplosion;

    public float delayDestroyFloat;
    
    void Awake()
    {
        AudioPickUpShield = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShield shield = collision.GetComponent<PlayerShield>();
            if (shield != null)
            {
                shield.ActivateShield(shieldDuration);
                AudioPickUpShield.Play();
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
