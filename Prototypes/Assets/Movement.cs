using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;

    Rigidbody2D rb2D;
    
    int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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


        if (Input.GetButton("Jump"))
        {
            // jump
            if (true)
            { // if on ground
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }
        
        
    }
}
