using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;               // Nombre que usas para identificar el sonido
        public AudioClip clip;            // El archivo de audio
        [Range(0f, 1f)] public float volume = 1f;
    }

    public Sound[] sounds; // Aquí puedes meter todos tus sonidos (hasta 8 o más)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                if (s.clip != null)
                {
                    AudioSource.PlayClipAtPoint(s.clip, Vector3.zero, s.volume);
                }
                else
                {
                    Debug.LogWarning("AudioManager: El clip está vacío para: " + name);
                }
                return;
            }
        }

        Debug.LogWarning("AudioManager: No se encontró ningún sonido con el nombre: " + name);
    }
}