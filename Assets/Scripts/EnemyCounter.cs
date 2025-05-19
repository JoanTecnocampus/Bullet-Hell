using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public Text counterText;   // Asignar desde el inspector
    public int EnemysLeft;
    public string sceneName;

    public static EnemyCounter instance;

    private void Start()
    {
        counterText = GameObject.Find("TextEnemyCount").GetComponent<Text>();
        UpdateText();
        /*GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        EnemysLeft = enemigos.Length;
        Debug.Log("Cantidad de enemigos: " + EnemysLeft);*/
        counterText.text = EnemysLeft.ToString();
    }

    private void Update()
    {
        
    }

    // private void Start()
    // {
    //     counterText.color = Color.white;
    //     GameObject go = GameObject.Find("TextEnemyCount");
    //     counterText = go.GetComponent<Text>();
    //     counterText.text = "Probando número: " + 99;
    // }
    
    void Awake()
    {
        // Singleton simple para poder acceder desde Enemy
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKill()
    {
        EnemysLeft--;
        UpdateText();
    }

    void UpdateText()
    {
        if (counterText != null)
        {
            Debug.Log("Enemys Left: " + EnemysLeft);
            //counterText.text = "Enemys Left: " + EnemysLeft;
            counterText.text = EnemysLeft.ToString();
        }

        if (EnemysLeft == 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}