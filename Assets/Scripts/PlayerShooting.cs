using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePointx2;
    public Transform firePointx3;
    public float bulletSpeed = 15f;
    public float fireRate = 0.10f;
    public bool doubleShoot = false;

    private Vector2 shootDirection;
    private float fireCooldown = 0f;

    void Update()
    {
        shootDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            shootDirection += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow))
            shootDirection += Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow))
            shootDirection += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow))
            shootDirection += Vector2.right;

        fireCooldown -= Time.deltaTime;

        if (shootDirection != Vector2.zero && fireCooldown <= 0f)
        {
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Shoot(shootDirection.normalized);
            fireCooldown = fireRate;
        }
    }

    public void Shoot(Vector2 direction)
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        // Si doble disparo está activado, dispara también desde firePointx2
        if (doubleShoot == true)
        {
            Debug.Log("Entrado en doubleshoot");
            GameObject firePointx2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, firePointx2.transform.position, Quaternion.identity);
            bullet2.transform.rotation = Quaternion.Euler(0, 0, angle);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
                rb2.linearVelocity = direction * bulletSpeed;
        }
    }

    public void activarx2()
    {
        firePointx2.gameObject.SetActive(true);
        //firePointx2.gameObject.SetActive(false);
    }
    public void activarx3()
    {
        firePointx3.gameObject.SetActive(true);
        //firePointx3.gameObject.SetActive(false);
    }
}
