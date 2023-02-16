using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public enum State
    {
        Idle, //does nothing until player is in range
        Approach,//starts to go towards player until in range of attack
        PouncePrep,//charges pounce attack up
        DashPrep,//charges dash attack up
        Pounce,//leaps toward player, deals damage if hit
        Dash//dashes toward player, deals damage if hit
    }

    // current state
    public State state = State.Idle;
     // enemy health
    public int health = 3;
    public float damageDuration = 1;
    private float damageTimer;
    private bool isRed = false;
    // state change ranges
    public float idleRange; // range needed for idle to end 
    public float approachRange; // range needed for approach to end
    // movement and pounce speeds
    public float speed = 4;
    public float pounceChance = 0.5f;
    public float pounceSpeedVertical = 7;
    public float pounceSpeedHorizontal = 10;
    public float dashSpeed = 10;
    public float prepareDuration = 1.0f; // time to wait before pounce
    public float slowdownMeleeCollideAmount = 0.8f;
    public float ricochetMeleeAmount = 0.7f;
    public float slowdownPounceCollideAmount = 0.8f;
    private float prepareTimer;
    public AudioClip approachSound;
    public AudioClip dashPrepSound;
    public AudioClip pouncePrepSound;
    public AudioClip[] hurtSounds;
    public AudioClip meleeParry;
    public AudioClip[] deathSounds;
    public GameObject deathObject;
    private AudioSource audioSource;

    // reference to player
    private GameObject player;
    // references to enemy components
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float direction = -1; // negative for left, positive for right
    private bool facingLeft = true; // starts facing left
    private bool isJumping = false;
    private bool pounceCollide;
    private bool dashStart = false;
    //so only takes one melee hit per player attack
    private PlayerMelee pm;
    private bool hasTakenMelee = false;
    private float meleeTimer;

    //for checking if landed
    private float lastYPosition;
    private bool playerCollide;
    private bool meleeCollide;

    //current colour
    private Color color;
   
    // reference to the fangs
    GameObject fangs;

    //starting position for checkpoints
    public Vector2 startingPosition;

    //animation stuff
    private Animator anim;
    private bool startDash = false;
    private bool startPounce = false;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); 
        rb = GetComponent<Rigidbody2D>();
        pm = player.GetComponent<PlayerMelee>();
        fangs = transform.GetChild(0).gameObject;

        startingPosition = rb.transform.position;
        //Debug.Log("start: " + startingPosition);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        damageDuration = 0.3f;
        isJumping = false;
        pounceCollide = false;
        meleeCollide = false;
        playerCollide = false;
        hasTakenMelee = false;
        startDash = false;
        startPounce = false;
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255f, 255f, 255f, 1f);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Approach:
                Approach();
                break;
            case State.PouncePrep:
                PouncePrep();
                break;
            case State.DashPrep:
                DashPrep();
                break;
            case State.Pounce:
                Pounce();
                break;
            case State.Dash:
                Dash();
                break;
        }

        if (isRed){
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0){
                sr.color = color;
                isRed = false;
            }
        }

        if (hasTakenMelee){
            if (pm.GetAttackLeft() <= 0){
                hasTakenMelee = false;
            }
        }
        }else{
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    void SwitchState(State nextState){
        
        if(!dead){
            state = nextState;
            switch (nextState)
        {
            case State.Idle:
                sr.color = new Color(255f, 255f, 255f, 1f);
                color = sr.color;
                break;
            case State.Approach:
                color = sr.color;
                break;
            case State.PouncePrep:
                audioSource.PlayOneShot(pouncePrepSound, 0.2f);
                sr.color = Color.yellow;
                color = sr.color;
                prepareTimer = prepareDuration;
                break;
            case State.Pounce:
                //sr.color = new Color(0, 255f, 0f, 1f);
                break;
            case State.DashPrep:
                audioSource.PlayOneShot(dashPrepSound, 0.3f);
                sr.color = Color.blue;
                color = sr.color;
                prepareTimer = prepareDuration;
                break;
            case State.Dash:
                //sr.color = new Color(255f, 255f, 0f, 1f);
                break;
        }
        }
        
    }

    void Idle()
    {
        
        if(!dead){
            if(Vector2.Distance(player.transform.position, transform.position) <= idleRange)
        {
            audioSource.PlayOneShot(approachSound, 0.25f);
            SwitchState(State.Approach);
        }
        }
    }

    void Approach()
    {
        if(!dead){
            anim.SetTrigger("approach");
        CheckFacing();
        Vector2 velocity = new Vector2(direction * speed, rb.velocity.y);
        rb.velocity = velocity;
        if(Vector2.Distance(player.transform.position, transform.position) <= approachRange)
        {
            rb.velocity = new Vector2(0, 0);
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= pounceChance){
                SwitchState(State.PouncePrep);
            } else {
                SwitchState(State.DashPrep);
            }
        }
        }
    }

    void PouncePrep()
    {
        
            CheckFacing();
        if(!startPounce){
            anim.SetTrigger("pouncePrep");
            startPounce = true;
        }
        prepareTimer -= Time.deltaTime;
        if (prepareTimer <= 0){
            SwitchState(State.Pounce);
        }
    } 
        

    void Pounce()
    {
        
        
            if (rb.velocity.y == 0 && !isJumping){
            anim.SetTrigger("pounce");
           rb.velocity = new Vector2(direction * pounceSpeedHorizontal, pounceSpeedVertical);
           fangs.SetActive(true);
           isJumping = true;
           startPounce = false;
        }
        else if (rb.velocity.y == 0 && isJumping){
            isJumping = false;
            fangs.SetActive(false);
            pounceCollide = false;
            SwitchState(State.Approach);         
        }
        
    }

    void DashPrep()
    {
        
            CheckFacing();
        if(!startDash){
            anim.SetTrigger("dashPrep");
            startDash = true;
        }
        prepareTimer -= Time.deltaTime;
        if(prepareTimer <= 0){
            SwitchState(State.Dash);
        }
        
        
    }

    void Dash(){
        
       
            if (!dashStart){
            anim.SetTrigger("dash");
            dashStart = true;
            startDash = false;
            rb.velocity = new Vector2(direction * dashSpeed, 0);
            fangs.SetActive(true);
        }
        else if (rb.velocity.x == 0 && dashStart){
            dashStart = false;
            fangs.SetActive(false);
            SwitchState(State.Approach);
        }
        
        
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        direction *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckFacing()
    {
        if (player.transform.position.x > transform.position.x){
            direction = 1;
            if (facingLeft){
                Flip();
            }
        }
        else if(player.transform.position.x < transform.position.x){
            direction = -1;
            if (!facingLeft){
                Flip();
            }
        }
        else{
            direction = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(state == State.Pounce && !pounceCollide){
            pounceCollide = true;
            rb.velocity *= slowdownPounceCollideAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag =="Player" && !dead){
            playerCollide = true;
            if (state == State.Dash){
                rb.velocity *= slowdownMeleeCollideAmount;
            }
        }
        else if(other.tag =="MeleeWeapon"){
            meleeCollide = true;
        }
        else if(other.tag == "Bullet" && !dead)
        {
            Destroy(other.gameObject);
            health -= 1;
            if(health <= 0){
                //Destroy(this.gameObject);
                //dont destroy so checkpoint can revive them
                if(state != State.Pounce && !dead){
                    //on the ground
                    //Debug.Log("norm death: " + state);
                    anim.SetTrigger("normalDeath");
                }else if (state == State.Pounce && !dead){
                   
                     //Debug.Log("air death: " + state);
                     anim.SetTrigger("airDeath");
                }
                Death();
                //gameObject.SetActive(false);
            }else if(state == State.Idle){
                SwitchState(State.Approach);
            }else{   
                sr.color = new Color(255f, 0f, 0f, 1f);
                isRed = true;
                damageTimer = damageDuration;
                int randomValue = Random.Range(0, hurtSounds.Length);
                audioSource.PlayOneShot(hurtSounds[randomValue], 0.25f);
            }
        }
        if(!playerCollide && meleeCollide && !hasTakenMelee && !dead)
        {
            health -= 1;
            if(health <= 0){
                //Destroy(this.gameObject);
                //dont destroy for checkpoint
                if(!isJumping && !dead){
                    //on the ground
                    Debug.Log("melee death");
                    anim.SetTrigger("meleeDeath");
                }else if ( state == State.Pounce && !dead){
                    Debug.Log("air2 death");
                    anim.SetTrigger("airDeath");
                }
                Death();
            } 
            else{
                sr.color = new Color(255f, 0f, 0f, 1f);
                isRed = true;
                damageTimer = damageDuration;
                int randomValue = Random.Range(0, hurtSounds.Length);
                audioSource.PlayOneShot(hurtSounds[randomValue], 0.25f);
                if (state == State.Dash){
                    audioSource.PlayOneShot(meleeParry, 0.25f);
                    rb.velocity *= -ricochetMeleeAmount;
                    Flip();
                }
                hasTakenMelee = true;
            }
        }
    }

    public void Death(){
        sr.color = new Color(255f, 255f, 255f, 1f);
        dead = true;
        anim.SetBool("dead", true);
        GameObject deathObj = Instantiate(deathObject);
        AudioSource deathAudio = deathObj.GetComponent<AudioSource>();
        int randomValue = Random.Range(0, deathSounds.Length);
        deathAudio.clip = deathSounds[randomValue];
        deathAudio.Play();
    }

    //so death animation can play before obj is invis
    public void Invisible(){
        gameObject.SetActive(false);
        dead = false;
        anim.SetBool("dead", false);
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player"){
            playerCollide = false;
        }
        else if (other.tag == "MeleeWeapon"){
            meleeCollide = false;
        }
    }

    public void resetPosition(){
        health = 3;
        rb.transform.position = startingPosition;
        rb.velocity = new Vector2(0, 0);
        if(!facingLeft){
            Flip();
        }
        SwitchState(State.Idle);
        gameObject.SetActive(true);
    }

    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }
}