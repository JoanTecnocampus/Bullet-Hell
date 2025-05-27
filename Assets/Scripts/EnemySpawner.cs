using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyNormal;
    public GameObject enemyTank;
    public GameObject enemySniper;
    public Transform[] spawnPoints;
    /*public Transform enemySpawn;
    public Transform enemySpawn1;
    public Transform enemySpawn2;
    public Transform enemySpawn3;
    public Transform enemySpawn4;
    public Transform enemySpawn5;*/
    public float spawnInterval = 2f;       // Tiempo entre spawns
    public int maxEnemies = 10;            // Máximo de enemigos que se pueden spawnear
    
    public int enemiesSpawned = 0;
    
    public int randomEnemy;
    
    public static EnemySpawner instance;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        
    }
    
    /*void chooceSpawner()
    {
        // Random del 1 al 6 para decidir si se genera el PowerUp
        randomSpawner = Random.Range(1, 7);
        Debug.Log("Número aleatorio random Spawner: " + randomSpawner);

        switch (randomSpawner)
        {
            case 1:
                Debug.Log("Salió el 1");
                SpawnEnemy(enemySpawn);
                break;
            case 2:
                Debug.Log("Salió el 2");
                SpawnEnemy(enemySpawn1);
                break;
            case 3:
                Debug.Log("Salió el 3");
                SpawnEnemy(enemySpawn2);
                break;
            case 4:
                Debug.Log("Salió el 4");
                SpawnEnemy(enemySpawn3);
                break;
            case 5:
                Debug.Log("Salió el 5");
                SpawnEnemy(enemySpawn4);
                break;
            case 6:
                Debug.Log("Salió el 6");
                SpawnEnemy(enemySpawn5);
                break;
            default:
                Debug.Log("Número fuera de rango");
                break;
        }
    }*/

    void SpawnEnemy()
    {
        if (enemiesSpawned >= maxEnemies) return;
        
        // Random del 1 al 3 para decidir cual PowerUp se genera
        randomEnemy = Random.Range(1, 4);
        Debug.Log("Número aleatorio Generar Enemy: " + randomEnemy);
        
        switch (randomEnemy)
        {
            case 1:
                Debug.Log("Salió el 1");
                generateEnemy(enemyNormal);
                break;
            case 2:
                Debug.Log("Salió el 2");
                generateEnemy(enemyTank);
                break;
            case 3:
                Debug.Log("Salió el 3");
                generateEnemy(enemySniper);
                break;
            default:
                Debug.Log("Número fuera de rango");
                break;
        }
        
    }
    
    void generateEnemy(GameObject enemy)
    {
        if (enemiesSpawned >= maxEnemies) return;

        // Elegir un punto aleatorio
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instanciar el enemigo
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

        enemiesSpawned++;
        
        // if (pickUp != null)
        // {
        //     Instantiate(pickUp, transform.position, Quaternion.identity);
        // }
        // else
        // {
        //     Debug.LogWarning("No se asignó un prefab al spawner.");
        // }
    }
}
