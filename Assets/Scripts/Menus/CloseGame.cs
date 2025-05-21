using UnityEngine;

public class CloseGame : MonoBehaviour
{
    
    public void ExitGame()
    {
        PauseGame descongelarJuego = GetComponent<PauseGame>();
        if (descongelarJuego != null)
        {
            descongelarJuego.ResumeGame();
        }
        // Esto funciona en una build del juego (EXE, APK, etc.)
        Application.Quit();

        // Esto es solo para probar en el editor de Unity (opcional)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}