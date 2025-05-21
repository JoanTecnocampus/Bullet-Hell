using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Asigna aquí el Canvas del menú de pausa
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa el juego
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el juego
            pauseMenuCanvas.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }
}