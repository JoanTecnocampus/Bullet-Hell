using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveInput.y = 1;
        if (Input.GetKey(KeyCode.S))
            moveInput.y = -1;
        if (Input.GetKey(KeyCode.D))
            moveInput.x = 1;
        if (Input.GetKey(KeyCode.A))
            moveInput.x = -1;

        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
