using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Flechas o A/D
        float moveY = Input.GetAxisRaw("Vertical");   // Flechas o W/S

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}