using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;           // Walk speed
    public float jumpForce = 7f;       // Jump strength
    public float boostForce = 15f;     // How strong the boost is

    private Rigidbody2D rb;            // Player body
    private int jumpsUsed = 0;         // How many jumps already used
    private int maxJumps = 3;          // Max jumps allowed (triple jump)

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

        // --- JUMP (up to 3 times before landing) ---
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsUsed < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Jump
            jumpsUsed++; // Add to jumps used
        }

        // --- BOOST (short burst of force) ---
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            float direction = Mathf.Sign(rb.linearVelocity.x); // Which way you're moving
            if (direction != 0) // Only boost if moving left or right
            {
                rb.AddForce(new Vector2(direction * boostForce, 0), ForceMode2D.Impulse);
            }
        }
    }

    // Reset jumps when touching the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsUsed = 0; // Back to 0 jumps when you land
        }
    }
}