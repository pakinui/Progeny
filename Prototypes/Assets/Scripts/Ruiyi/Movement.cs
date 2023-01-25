using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;
    public int maxJump = 30;
    private int currentJump = 0;
    public float jumpCD = 0.1f;
    private float currentJumpCD = 0f;

    Rigidbody2D rb2D;
    public Collider2D footCollider;
    
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentJump = 0;
        currentJumpCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // left right move
        float horizontalAxis = Input.GetAxis("Horizontal");
        int horizontalAxisRaw = (int)Input.GetAxisRaw("Horizontal");

        if (horizontalAxisRaw != 0)
        {
            // sDebug.Log(horizontalAxis + " " + horizontalAxisRaw);
            if (direction != horizontalAxisRaw)
            {
                transform.Rotate(new Vector3(0, 180f, 0));
                direction = horizontalAxisRaw;
            }
        }

        rb2D.velocity = new Vector2(horizontalAxis, rb2D.velocity.y);

        if (footCollider.IsTouchingLayers(Physics2D.AllLayers)) {
            currentJump = 0;
        }

        if (currentJumpCD > 0) {
            currentJumpCD -= Time.deltaTime;
        }
        if (Input.GetButton("Jump"))
        {
            // jump
            Debug.Log("Jumpcl");
            if (currentJump < maxJump && currentJumpCD <= 0)
            { // if on ground
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
                currentJump++;
                currentJumpCD = jumpCD;
               
            }
        }
        
        
    }
}
