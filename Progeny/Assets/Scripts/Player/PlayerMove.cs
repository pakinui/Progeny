using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // reference to the 'Player' script
    public Player player;
    // reference to the RigidBody component
    public Rigidbody2D rb;

    public Renderer render;

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
        // check if the player is falling
        // if(rb.velocity.y < 0f && !player.isFalling()) {
        //     Debug.Log("falling"); //says falling while walking??
        //     player.setFalling(true);
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // } else if(rb.velocity.y >= 0 && player.isFalling()) {
        //     player.setFalling(false);
        // }

        // horizontal movement input
        float direction = Input.GetAxis("Horizontal");
        if(player.isAllowedMovement()){
            // horizontal movement
            if(!player.isClimbing() && !player.isFalling() && direction != 0)
            {
                player.setMoving(true);
                rb.velocity = new Vector2(direction * player.GetCurrentSpeed(), rb.velocity.y);
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

    public void stopVelocity(){
        rb.velocity = new Vector2(0, 0);
    }

}
