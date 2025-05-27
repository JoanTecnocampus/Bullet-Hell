using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public Text counterText;   // Asignar desde el inspector
    private int EnemysLeft;
    public int EnemysTotal;
    public string sceneName;

    public static EnemyCounter instance;

    void Start()
    {
        counterText = GameObject.Find("TextEnemyCount").GetComponent<Text>();
        UpdateText();
    }

    /*private void Update()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        EnemysLeft = enemigos.Length;
        Debug.Log("Cantidad de enemigos: " + EnemysLeft);
        counterText.text = EnemysLeft.ToString();
        UpdateText();
    }*/

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

    public void AddEnemy()
    {
        EnemysTotal++;
        EnemysLeft++;
        UpdateText();
    }

    public void RemoveEnemy()
    {
        EnemysLeft--;
        UpdateText();
    }

    public void UpdateEnemyCounter()
    {
        EnemysLeft = EnemysTotal;
        Debug.Log("Cantidad de enemigos: " + EnemysLeft);
        counterText.text = EnemysLeft.ToString();
        UpdateText();
    }

    public void AddKill()
    {
        EnemysTotal = EnemySpawner.instance.enemiesSpawned;
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

        if (EnemysLeft == 0 && EnemysTotal == 10)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
