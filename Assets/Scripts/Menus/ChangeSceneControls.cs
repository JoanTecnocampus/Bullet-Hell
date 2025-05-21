using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneControls : MonoBehaviour
{
    // Cambiar por el nombre de la escena a la que quieres ir
    public string sceneName;
    
    public void SceneChanger()
    {
        PauseGame descongelarJuego = GetComponent<PauseGame>();
        if (descongelarJuego != null)
        {
            descongelarJuego.ResumeGame();
        }
        SceneManager.LoadScene(sceneName);
    }
}