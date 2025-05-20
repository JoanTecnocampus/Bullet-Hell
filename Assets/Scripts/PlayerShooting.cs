using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float fireRate = 0.05f;

    private Vector2 shootDirection;
    private float fireCooldown = 0f;

    private bool reboteActivo = false;
    private float reboteTiempoRestante = 0f;

    public Transform firePointx2;
    public Transform firePointx3;
    public bool doubleShoot = false;
    public bool tripleShoot = false;

    void Update()
    {
        shootDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow)) shootDirection += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow)) shootDirection += Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow)) shootDirection += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow)) shootDirection += Vector2.right;

        fireCooldown -= Time.deltaTime;

        if (shootDirection != Vector2.zero && fireCooldown <= 0f)
        {
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Shoot(shootDirection.normalized);
            fireCooldown = fireRate;
        }

        if (reboteActivo)
        {
            reboteTiempoRestante -= Time.deltaTime;
            if (reboteTiempoRestante <= 0f)
                reboteActivo = false;
        }
    }

    void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Bala principal
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.canBounce = reboteActivo;
        }

        if (doubleShoot)
        {
            GameObject bullet2 = Instantiate(bulletPrefab, firePointx2.position, Quaternion.identity);
            bullet2.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
                rb2.linearVelocity = direction * bulletSpeed;

            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            if (bulletScript2 != null)
                bulletScript2.canBounce = reboteActivo;
        }

        if (tripleShoot)
        {
            GameObject bullet2 = Instantiate(bulletPrefab, firePointx2.position, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, firePointx3.position, Quaternion.identity);

            bullet2.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            bullet3.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
                rb2.linearVelocity = direction * bulletSpeed;

            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            if (rb3 != null)
                rb3.linearVelocity = direction * bulletSpeed;

            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            if (bulletScript2 != null)
                bulletScript2.canBounce = reboteActivo;

            Bullet bulletScript3 = bullet3.GetComponent<Bullet>();
            if (bulletScript3 != null)
                bulletScript3.canBounce = reboteActivo;
        }
    }

    public void activarx2()
    {
        firePointx2.gameObject.SetActive(true);
        Debug.Log("Activado doubleshoot");

        if (tripleShoot)
        {
            tripleShoot = false;
            Debug.Log("Desactivado tripleShoot");
        }

        doubleShoot = true;
        StartCoroutine(DisableDoubleShootAfterDelay(5f));
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

        if (doubleShoot)
        {
            doubleShoot = false;
            Debug.Log("Desactivado doubleShoot");
        }

        tripleShoot = true;
        StartCoroutine(DisableTripleShootAfterDelay(5f));
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

    public void ActivarRebote(float duracion)
    {
        reboteActivo = true;
        reboteTiempoRestante = duracion;
    }
}
