using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float fireRate = 0.5f;

    private Vector2 shootDirection;
    private float nextFireTime = 0f;

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

        if (shootDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (Time.time >= nextFireTime)
            {
                Shoot(shootDirection.normalized);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}
