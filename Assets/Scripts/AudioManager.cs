using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource AudioBackground;
    
    
    private void Awake()
    {
        AudioBackground = GetComponent<AudioSource>();
        AudioBackground.Play();
    }
}