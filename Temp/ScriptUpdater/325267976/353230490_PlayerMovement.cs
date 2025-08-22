using UnityEngine;
using UnityEngine.InputSystem; // New Input System

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public float boostForce = 15f;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;     // put a child transform at the feet
    [SerializeField] float groundRadius = 0.15f;
    [SerializeField] LayerMask groundLayer;     // set to your Ground layer

    Rigidbody2D rb;
    int jumpsUsed = 0;
    const int maxJumps = 3;                     // total jumps per airtime (triple jump)
    int lastDir = 1;                            // remember facing for boost

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        bool left = kb.leftArrowKey.isPressed;
        bool right = kb.rightArrowKey.isPressed;

        // left / right
        if (left)  { rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y); lastDir = -1; }
        if (right) { rb.linearVelocity = new Vector2( speed, rb.linearVelocity.y); lastDir =  1; }
        if (!left && !right) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // triple jump: after 3, you must touch ground
        if (kb.upArrowKey.wasPressedThisFrame)
        {
            if (grounded) { jumpsUsed = 0; Jump(); }          // first jump from ground
            else if (jumpsUsed < maxJumps) { Jump(); }        // midair jumps 2 & 3
        }

        // boost: instant burst in facing direction (even from idle)
        if (kb.downArrowKey.wasPressedThisFrame)
            rb.AddForce(new Vector2(lastDir * boostForce, 0f), ForceMode2D.Impulse);

        void Jump()
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsUsed++;
        }
    }
}