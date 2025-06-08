using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource AudioBackground;
    
    
    private void Awake()
    {
        AudioBackground = GetComponent<AudioSource>();
        AudioBackground.Play();
    }
    void Start()
    {
        AudioBackground = GetComponent<AudioSource>();
        AudioBackground.loop = true;
        if (!AudioBackground.isPlaying)
        {
            AudioBackground.Play();
        }
    }
}