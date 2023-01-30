using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player states
    private bool isMoving = false;
    private bool isCrouching = false;
    private bool isClimbing = false;
    private bool isShooting = false;
    private bool isReloading = false;

    // movement speed
    // will increase/decrease depending on player state
    public float movementSpeed = 4f;

    // direction of player.
    // + is right, - is left.
    int direction = 1;

    // reference to the RigidBody
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement input
        float horizontalAxis = Input.GetAxis("Horizontal");

        // movement
        if(!isClimbing && horizontalAxis != 0)
        {
            isMoving = true;
            rb.velocity = new Vector2(horizontalAxis * movementSpeed, rb.velocity.y);
        }
        else
        {
            isMoving = false;
        }
    }
}
