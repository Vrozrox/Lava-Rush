using UnityEngine;

public class dragonScript : MonoBehaviour
{ 
    public Rigidbody2D rb;
    private float move;
    public float movementSpeed = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump!");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15f);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move * movementSpeed, rb.linearVelocity.y);
    }
}