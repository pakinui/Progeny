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
    public bool moving, crouching, climbing, vaulting, falling, pushing, hitting, aiming, shooting = false;
    // default movement speed of player
    public float movementSpeed;

    public AudioClip[] climbAudio;
    // sounds the player makes when hurt
    public AudioClip[] hurtSounds;

    public AudioClip deathSound;

    // reference to the player's gun object
    public Gun gun;
    private GameObject arm;

    PlayerShoot playerShoot;
    PlayerMelee playerMelee;
    PlayerMove playerMove;

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

    public ReturnToCheckpoint returnPlayer;

    public Vector3 climbPosition;//where the player should move after climbing

    private PlayerAnimationController ac;

    public float playerWidth;
    public float playerHeight;
    
    public bool dead = false;

    // true if they die after choosing to spare their kid
    // need so the death screen isnt triggered but the ending screen is
    public bool finalDecisionMade = false; 
    public iPad ipad;

    public GameObject endObject;
    private GameEnd gameEnd;
    
    //public GamePanelController gpc;
    private HealthBar healthBar;
    public float playerZPosition; //this should never change for parallax
    

    // Start is called before the first frame update
    public void Start(){
        currentSpeed = movementSpeed;
        currentHealth = maxHealth;

        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
       //gpc = GameObject.Find("GamePanel").GetComponent<GamePanelController>();

        arm = GameObject.Find("player-arm");
        arm.SetActive(false);

        playerShoot = GetComponent<PlayerShoot>();
        playerMelee = GetComponent<PlayerMelee>();
        playerMove = GetComponent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        ac = GetComponent<PlayerAnimationController>();

        playerZPosition = transform.position.z;
        gameEnd = endObject.GetComponent<GameEnd>();
        
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
        /**
        GUI.Label(new Rect(30,30, 100, 100), "Health: " + currentHealth.ToString());
        GUI.Label(new Rect(30,45, 200, 100), "Melee Cooldown: " + playerMelee.GetCooldownLeft().ToString("0.0"));
        if(gun != null) {GUI.Label(new Rect(30,60, 200, 100), "Shot Cooldown: " + playerShoot.GetCooldownLeft().ToString("0.0"));}
        */
    }

    // method to flip the player
    public void Flip()
    {
        // set the direction variable
        facingRight = !facingRight;
        // flip the character
        transform.Rotate(0f, 180f, 0f);
        // flip the gun and arm
        if(gun != null){
            gun.transform.Rotate(180f, 0f, 0f);
            arm.transform.Rotate(180f, 0f, 0f);
        }
        // flip the ledge indicator boxes
        //GetComponent<PlayerClimb>().xOffset *= -1;
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
        if (currentHealth > health && health > 0 && !dead){
            int randomValue = Random.Range(0, hurtSounds.Length);
            audioSource.PlayOneShot(hurtSounds[randomValue], 0.25f);
        }
        else if (health <= 0 && !dead){
            
            
            audioSource.PlayOneShot(deathSound, 0.5f);
            health = 0;//to make sure health doesnt go below 0
            ac.anim.SetTrigger("death");
            dead = true;
            Debug.Log("trigger death");
        }
        if(!dead){
            currentHealth = health;
        }
        Debug.Log("curr health: " + health);
        
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
            arm.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f);
        }else{
            sr.color = new Color(255f, 255f, 255f, 1f);
            arm.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
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
            // move aim rotation point (and therefore arm+gun)
            arm.transform.parent.localPosition = new Vector2(arm.transform.parent.localPosition.x + 0.08f, 0.2f);
        }
        else{
            currentSpeed = movementSpeed;
            arm.transform.parent.localPosition = new Vector2(arm.transform.parent.localPosition.x - 0.08f, 0.52f);
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
    public void setHitting(bool x){
        hitting = x;
        if(x == true) {
            combatTimer = outOfCombatDuration;
        }}

    public bool isAiming(){return aiming;}
    public void setAiming(bool x)
    {
        aiming = x;
        if(x == true) {
            gun.gameObject.SetActive(true);
            arm.SetActive(true);
        } else {
            if(gun != null)gun.gameObject.SetActive(false);
            arm.SetActive(false);
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

    public bool isAllowedMovement(){return allowMovement;}
    public void setAllowedMovement(bool x){allowMovement = x;}

    // to stop both allowing movement and movement animations
    public void stopPlayerMovement(){
        allowMovement = false;
        moving = false;
        playerMelee.setMelee(false);
        playerMove.stopVelocity();
        setAiming(false);
    }
    // to start both allowing movement and movement animations
    public void startPlayerMovement(){
        allowMovement = true;
        moving = true;
        playerMelee.setMelee(true);
        hitting = false;
    }

    public void gotGun(){
        audioSource.PlayOneShot(gun.pickupSound, 0.25f);
    }

    public void NoHealth(){
        stopPlayerMovement();
        currentHealth = 0;
        Debug.Log("no health");
        
    }
    public void Die()
    {
        //canvas.deathPanel.SetActive(true);
        Debug.Log("current Health :" + currentHealth);

        if(finalDecisionMade){
            //trigger end panel not death panel
            
                endObject.SetActive(true);
                gameEnd.EndDeathScreen();
                Time.timeScale = 0;
            

        }else{
            --currentHealth;
        
            ac.anim.SetTrigger("resetToIdle");
        }
        
        //currentHealth = maxHealth;
        


    }

    public void ClimbAction(){
        transform.position = climbPosition;
        climbing = false;
        startPlayerMovement();
    }

    // public void StrikeAction(){
    //     playerMelee.Swing();
    // }



    public void resetPlayer(){
        
        currentHealth = maxHealth;
        Debug.Log("reset player :" + currentHealth);
        transform.position = gm.getLastCheckpoint();
        
        startPlayerMovement();
        moving = false;
        crouching = false;
        climbing = false;
        vaulting = false;
        falling = false;
        pushing = false;
        hitting = false;
        aiming = false;
        shooting = false;
        dead = false;
        //Debug.Log("currasdent Health :" + currentHealth);
        healthBar.ResetHealthbar();
        //Debug.Log("cuqqrrent Health :" + currentHealth);
    }

   
}