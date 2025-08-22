using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;        // How fast you walk
    public float jumpForce = 7f;    // How high you jump
    public float boostMultiplier = 2f; // Boost makes you go faster

    private Rigidbody2D rb;         // Lets us push the player with physics
    private int jumpsLeft = 3;      // How many jumps you still have (triple jump = 3)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Grab the Rigidbody2D from your player
    }

    void Update()
    {
        // --- LEFT AND RIGHT ---
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y); // Move left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);  // Move right
        }

        // Stop if no arrow pressed
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // --- JUMP ---
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Jump up
            jumpsLeft--; // Use up one jump
        }

        // --- BOOST ---
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float direction = Mathf.Sign(rb.linearVelocity.x); // -1 if going left, +1 if going right
            rb.linearVelocity = new Vector2(speed * boostMultiplier * direction, rb.linearVelocity.y);
        }
    }

    // Reset jumps when you touch the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 3; // Back to 3 jumps when you land
        }
    }
}