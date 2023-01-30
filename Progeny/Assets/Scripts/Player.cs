using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // health of player
    public int health = 100;
    // direction of player.
    private bool facingRight = true;
    // player states
    private bool moving, crouching, climbing, aiming, shooting, reloading = false;

    // Start is called before the first frame update
    public void Start(){}
    // Update is called once per frame
    public void Update(){}

    // method to flip the Player object and update the direction variable
    public void Flip()
    {
        facingRight = !facingRight;
        //transform.Rotate(0f, 180f, 0f);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    // direction variable accessor
    public bool isFacingRight(){return facingRight;}
    
    // player state accessor and mutator methods
    public bool isMoving(){return moving;}
    public void setMoving(bool x){moving = x;}

    public bool isCrouching(){return crouching;}
    public void setCrouching(bool x){crouching = x;}

    public bool isClimbing(){return climbing;}
    public void setClimbing(bool x){climbing = x;}

    public bool isAiming(){return aiming;}
    public void setAiming(bool x){aiming = x;}

    public bool isShooting(){return shooting;}
    public void setShooting(bool x){shooting = x;}

    public bool isReloading(){return reloading;}
    public void setReloading(bool x){reloading = x;}
}
