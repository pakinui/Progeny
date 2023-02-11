using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // reference to the GameMaster component
    private GameMaster gm;
    // reference to respawn screen
    private Canvas canvas;
    // maxHealth of player
    public float maxHealth = 100f;
    // direction of player.
    private bool facingRight = true;
    // player states
    private bool moving, crouching, climbing, vaulting, falling, pushing, hitting, aiming, shooting, reloading = false;
    // default movement speed of player
    public float movementSpeed;

    // sounds the player makes when hurt
    public AudioClip[] hurtSounds;

    public AudioClip deathSound;

    // reference to the player's gun object
    public Gun gun;

    PlayerShoot playerShoot;
    PlayerMelee playerMelee;

    SpriteRenderer sr;
    private bool red = false;
    private float redTimer, redDuration = 0.5f;

    // variables to disable isShooting
    public float outOfCombatDuration;
    private float combatTimer;

    //current speed of player, can change depending on state.
    [SerializeField] private float currentSpeed;
    //current health
    [SerializeField] private float currentHealth;

    //boolean to prevent player from moving when needed (i.e. cutscenes)
    private bool allowMovement = true;
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start(){
        currentSpeed = movementSpeed;
        currentHealth = maxHealth;

        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        playerShoot = GetComponent<PlayerShoot>();
        playerMelee = GetComponent<PlayerMelee>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void Update(){
        if (isRed()){
            redTimer -= Time.deltaTime;
            if (redTimer <= 0){
                setRed(false);
            }
        }

        if(combatTimer > 0f){
            combatTimer -= Time.deltaTime;
        } else {
            setShooting(false);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(30,30, 100, 100), "Health: " + currentHealth.ToString());
        GUI.Label(new Rect(30,45, 200, 100), "Melee Cooldown: " + playerMelee.GetCooldownLeft().ToString("0.0"));
        if(gun != null) {GUI.Label(new Rect(30,60, 100, 100), "Ammo: " + gun.ammoLeft);}
        if(gun != null) {GUI.Label(new Rect(30,75, 200, 100), "Shot Cooldown: " + playerShoot.GetCooldownLeft().ToString("0.0"));}
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
        // flip the camera controller direction
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().direction *= -1;
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
        if (currentHealth > health && health > 0){
            int randomValue = Random.Range(0, hurtSounds.Length);
            audioSource.PlayOneShot(hurtSounds[randomValue], 0.25f);
        }
        else if (health <= 0){
            audioSource.PlayOneShot(deathSound, 0.5f);
        }
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
    public void setShooting(bool x){
        shooting = x;
        if(x == true) {
            combatTimer = outOfCombatDuration;
        }
    }
    public float getCombatTimer(){return combatTimer;}

    public bool isReloading(){return reloading;}
    public void setReloading(bool x){reloading = x;}

    public bool isAllowedMovement(){return allowMovement;}
    public void setAllowedMovement(bool x){allowMovement = x;}

    // to stop both allowing movement and movement animations
    public void stopPlayerMovement(){
        allowMovement = false;
        moving = false;
        setAiming(false);
    }
    // to start both allowing movement and movement animations
    public void startPlayerMovement(){
        allowMovement = true;
        moving = true;
    }

    public void gotGun(){
        audioSource.PlayOneShot(gun.pickupSound, 0.35f);
    }

    public void Die()
    {
        canvas.deathPanel.SetActive(true);
    }
}