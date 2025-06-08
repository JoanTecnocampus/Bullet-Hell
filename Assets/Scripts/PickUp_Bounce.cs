using System.Collections;
using UnityEngine;

public class PickUp_Bounce : MonoBehaviour
{
    public float duration = 5f;
    
    public AudioSource AudioPickUpBounce;
    //private AudioSource AudioEnemyExplosion;

    public float delayAudioFloat;
    public bool audioPlaying;

    public float delayDestroyFloat;
    
    void Awake()
    {
        AudioPickUpBounce = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.ActivarRebote(duration);
                AudioPickUpBounce.Play();
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
