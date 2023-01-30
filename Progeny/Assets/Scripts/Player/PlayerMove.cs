using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // reference to the 'Player' script
    Player player;
    // reference to the RigidBody component
    private Rigidbody2D rb;

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
        float direction = Input.GetAxis("Horizontal");

        // horizontal movement
        if(!player.isClimbing() && direction != 0)
        {
            player.setMoving(true);
            rb.velocity = new Vector2(direction * player.movementSpeed, rb.velocity.y);
        }
        else
        {
            player.setMoving(false);
        }

        // flip player in suitable direction
        if (direction > 0 && !player.isFacingRight())
        {
            player.Flip();
        }
        else if (direction < 0 && player.isFacingRight())
        {
            player.Flip();
        }
    }

}
