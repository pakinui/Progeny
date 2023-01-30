using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // reference to the 'Player' script
    Player player;
    // reference to the RigidBody component
    private Rigidbody2D rb;

    // movement speed multiplier
    // will increase/decrease depending on player state
    public float movementSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement input
        float horizontalAxis = Input.GetAxis("Horizontal");

        // horizontal movement
        if(!player.isClimbing && horizontalAxis != 0)
        {
            player.isMoving = true;
            rb.velocity = new Vector2(horizontalAxis * movementSpeed, rb.velocity.y);
        }
        else
        {
            player.isMoving = false;
        }
    }

}
