using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Cambiar por el nombre de la escena a la que quieres ir
    public string sceneName;

    public void SceneChanger()
    {
        SceneManager.LoadScene(sceneName);
    }
}