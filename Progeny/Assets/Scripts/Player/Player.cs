using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // maxHealth of player
    public float maxHealth = 100f;
    // direction of player.
    private bool facingRight = true;
    // player states
    private bool moving, crouching, climbing, vaulting, falling, pushing, aiming, shooting, reloading = false;
    // default movement speed of player
    public float movementSpeed;
    // reference to the player's gun object
    public GameObject gun;

    //current speed of player, can change depending on state.
    [SerializeField] private float currentSpeed;
    //current health
    [SerializeField] private float currentHealth;

    // Start is called before the first frame update
    public void Start(){
        currentSpeed = movementSpeed;
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    public void Update(){}

    private void OnGUI()
    {
        GUI.Label(new Rect(30,60, 100, 100), "Health: " + currentHealth.ToString());
    }

    // method to flip the player
    public void Flip()
    {
        // set the direction variable
        facingRight = !facingRight;
        // flip the character
        transform.Rotate(0f, 180f, 0f);
        // flip the gun
        if(gun != null){
            transform.GetChild(0).GetChild(0).Rotate(180f, 0f, 0f);
        }
        // flip the ledge indicator boxes
        GetComponent<PlayerClimb>().xOffset *= -1;
    }

    //getter for returning current speed of player.
    public float GetCurrentSpeed(){
        return currentSpeed;
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

    //adds health to value (subtracts if negative)
    public void SetCurrentHealth(float health){
        currentHealth = health;
    }
    
    // direction variable accessor
    public bool isFacingRight(){return facingRight;}
    
    // player state accessor and mutator methods
    public bool isMoving(){return moving;}
    public void setMoving(bool x){moving = x;}

    public bool isCrouching(){return crouching;}
    public void setCrouching(bool x){
        crouching = x;
        if (x == true){
            currentSpeed = movementSpeed/2f;
        }
        else{
            currentSpeed = movementSpeed;
        }
    }

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
            currentSpeed = movementSpeed/2f;
        } else {
            gun.SetActive(false);
            currentSpeed = movementSpeed;
        }
    }

    public bool isShooting(){return shooting;}
    public void setShooting(bool x){shooting = x;}

    public bool isReloading(){return reloading;}
    public void setReloading(bool x){reloading = x;}


    public void Die()
    {
        // temporary
        Destroy(this.gameObject);
        // TODO: create a proper death loop
    }
}