using System;
using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePointx2;
    public Transform firePointx3;
    public float bulletSpeed = 15f;
    public float fireRate = 0.10f;
    public bool doubleShoot = false;
    public bool tripleShoot = false;

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
        // Si doble disparo estÃ¡ activado, dispara tambiÃ©n desde firePointx2
        // Si doble disparo está activado, dispara también desde firePointx2
        if (doubleShoot == true)
        {
            //tripleShoot = false;
            //Debug.Log("Entrado en doubleshoot");
            //GameObject firePointx2 = Instantiate(bulletPrefab, firePointx2Pos.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, firePointx2.transform.position, Quaternion.identity);
            bullet2.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
                rb2.linearVelocity = direction * bulletSpeed;
        }
        // Si doble disparo estÃ¡ activado, dispara tambiÃ©n desde firePointx2
        // Si doble disparo está activado, dispara también desde firePointx2
        if (tripleShoot == true)
        {
            //doubleShoot = false;
            //Debug.Log("Entrado en tripleShoot");
            //GameObject firePointx3 = Instantiate(bulletPrefab, firePointx3Pos.position, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, firePointx3.transform.position, Quaternion.identity);
            bullet3.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            if (rb3 != null)
                rb3.linearVelocity = direction * bulletSpeed;
            GameObject bullet2 = Instantiate(bulletPrefab, firePointx2.transform.position, Quaternion.identity);
            bullet2.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
                rb2.linearVelocity = direction * bulletSpeed;
        }
    }

    public void activarx2()
    {
        firePointx2.gameObject.SetActive(true);
        Debug.Log("Activado doubleshoot");
        if (tripleShoot == true)
        {
            tripleShoot = false;
            Debug.Log("Desactivado tripleShoot");
        }
        StartCoroutine(DisableDoubleShootAfterDelay(5f));
        //firePointx2.gameObject.SetActive(false);
    }
    private IEnumerator DisableDoubleShootAfterDelay(float delay)
    {
        Debug.Log("Empieza contador doubleShoot");
        yield return new WaitForSeconds(delay);
        doubleShoot = false;
        firePointx2.gameObject.SetActive(false);
        Debug.Log("doubleShoot desactivado");
    }
    public void activarx3()
    {
        firePointx2.gameObject.SetActive(true);
        firePointx3.gameObject.SetActive(true);
        Debug.Log("Activado tripleShoot");
        if (doubleShoot == true)
        {
            doubleShoot = false;
            Debug.Log("Desactivado doubleShoot");
        }
        StartCoroutine(DisableTripleShootAfterDelay(5f));
        //firePointx3.gameObject.SetActive(false);
    }
    private IEnumerator DisableTripleShootAfterDelay(float delay)
    {
        Debug.Log("Empieza contador tripleShoot");
        yield return new WaitForSeconds(delay);
        tripleShoot = false;
        firePointx2.gameObject.SetActive(false);
        firePointx3.gameObject.SetActive(false);
        Debug.Log("tripleShoot desactivado");
    }
}
