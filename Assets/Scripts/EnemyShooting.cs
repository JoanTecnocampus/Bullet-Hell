using System;
using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public GameObject Player;
    public Transform firePointEnemy;
    public float bulletSpeed = 15f;
    public float fireRate = 1.0f;
    public bool shooting = true;

    public float shootingDistance = 5f; // Distancia mínima para disparar

    private float fireCooldown = 0f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!shooting || Player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (distanceToPlayer > shootingDistance)
            return; // Si está muy lejos, no dispara

        fireCooldown -= Time.deltaTime;

        Vector2 direction = (Player.transform.position - transform.position).normalized;

        Debug.DrawLine(transform.position, Player.transform.position, Color.red);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (fireCooldown <= 0f)
        {
            Shoot(direction);
            fireCooldown = fireRate;
        }
    }

    public void Shoot(Vector2 direction)
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, firePointEnemy.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Ajuste si tu sprite está "de pie"

        Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

    private IEnumerator DisableEnemyShoot(float delay)
    {
        Debug.Log("Shooting activo, se desactiva en " + delay + " segundos");
        yield return new WaitForSeconds(delay);
        shooting = false;
        Debug.Log("Disparo del enemigo desactivado");
    }
}
