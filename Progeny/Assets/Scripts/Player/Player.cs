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
    private bool moving, crouching, climbing, vaulting, falling, pushing, aiming, shooting, reloading = false;
    // movement speed multiplier
    // will increase/decrease depending on player state
    public float movementSpeed = 2f;
    // reference to the player's gun object
    public GameObject gun;

    // Start is called before the first frame update
    public void Start(){}
    // Update is called once per frame
    public void Update(){}

    // method to flip the Player object and update the direction variable
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        // flip the gun
        transform.GetChild(0).GetChild(0).Rotate(180f, 0f, 0f);
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

    public bool isVaulting(){return vaulting;}
    public void setVaulting(bool x){vaulting = x;}

    public bool isFalling(){return falling;}
    public void setFalling(bool x){falling = x;}

    public bool isPushing(){return pushing;}
    public void setPushing(bool x){pushing = x;}

    public bool isAiming(){return aiming;}
    public void setAiming(bool x)
    {
        aiming = x;
        if(x == true) {
            gun.SetActive(true);
            movementSpeed = 1f;
        } else {
            gun.SetActive(false);
            movementSpeed = 2f;
        }
    }

    public bool isShooting(){return shooting;}
    public void setShooting(bool x){shooting = x;}

    public bool isReloading(){return reloading;}
    public void setReloading(bool x){reloading = x;}
}
