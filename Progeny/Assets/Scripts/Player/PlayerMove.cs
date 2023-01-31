using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // reference to the 'Player' script
    private Player player;
    // reference to the RigidBody component
    public Rigidbody2D rb;

    // needed for climbing
    public Renderer render;

    // stops player from being able to move
    public static bool moving = true;
    

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is allowed to move
        if(moving){
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
            if (direction > 0 && !player.isAiming() && !player.isFacingRight())
            {
                player.Flip();
            }
            else if (direction < 0 && !player.isAiming() && player.isFacingRight())
            {
                player.Flip();
            }
        }
       
    }

    public void SetMovement(bool move){
        moving = move;
    }



}
