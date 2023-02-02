using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // maxHealth of player
    public float maxHealth = 100f;
    // direction of player.
    private bool facingRight = true;
    // player states
    private bool moving, crouching, climbing, vaulting, falling, pushing, hitting, aiming, shooting, reloading = false;
    // default movement speed of player
    public float movementSpeed;
    // reference to the player's gun object
    public Gun gun;

    PlayerShoot playerShoot;

    SpriteRenderer sr;
    private bool red = false;
    private float redTimer, redDuration = 1f;

    //current speed of player, can change depending on state.
    [SerializeField] private float currentSpeed;
    //current health
    [SerializeField] private float currentHealth;

    //boolean to prevent player from moving when needed (i.e. cutscenes)
    private bool allowMovement = true;

    // Start is called before the first frame update
    public void Start(){
        currentSpeed = movementSpeed;
        currentHealth = maxHealth;
        playerShoot = GetComponent<PlayerShoot>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    public void Update(){
        if (isRed()){
            redTimer -= Time.deltaTime;
            if (redTimer <= 0){
                setRed(false);
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(30,60, 100, 100), "Health: " + currentHealth.ToString());
        if(gun != null) {GUI.Label(new Rect(30,90, 100, 100), "Ammo: " + gun.ammoLeft);}
        if(gun != null) {GUI.Label(new Rect(30,120, 100, 100), "Shot Cooldown: " + playerShoot.GetCooldownLeft());}
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
    
    // red accessor
    public bool isRed(){return red;}
    // red mutator
    public void setRed(bool x)
    {
        red = x;
        if(x == true){
            redTimer = redDuration;
            sr.color = new Color(255f, 0f, 0f, 1f);
        }else{
            sr.color = new Color(255f, 255f, 255f, 1f);
        }
        
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

    public bool isHitting(){return hitting;}
    public void setHitting(bool x){hitting = x;}

    public bool isAiming(){return aiming;}
    public void setAiming(bool x)
    {
        aiming = x;
        if(x == true) {
            gun.gameObject.SetActive(true);
            currentSpeed = movementSpeed/2f;
        } else {
            gun.gameObject.SetActive(false);
            currentSpeed = movementSpeed;
        }
    }

    public bool isShooting(){return shooting;}
    public void setShooting(bool x){shooting = x;}

    public bool isReloading(){return reloading;}
    public void setReloading(bool x){reloading = x;}

    public bool isAllowedMovement(){return allowMovement;}
    public void setAllowedMovement(bool x){allowMovement = x;}

    // to stop both allowing movement and movement animations
    public void stopPlayerMovement(){
        allowMovement = false;
        moving = false;
    }
    // to start both allowing movement and movement animations
    public void startPlayerMovement(){
        allowMovement = true;
        moving = true;
    }


    public void Die()
    {
        // temporary
        //Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // TODO: create a proper death loop
    }
}