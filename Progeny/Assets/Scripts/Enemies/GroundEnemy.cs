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
    public float prepareDuration = 1.5f; // time to wait before pounce
    private float prepareTimer;
    // reference to player
    private GameObject player;
    // references to enemy components
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float direction = -1; // negative for left, positive for right
    private bool facingLeft = true; // starts facing left
    private bool isJumping = false;
    private bool dashStart = false;
   
    // reference to the fangs
    GameObject fangs;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        fangs = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
                sr.color = new Color(255f, 255f, 255f, 1f);
                isRed = false;
            }
        }
    }

    void SwitchState(State nextState){
        state = nextState;
        switch (nextState)
        {
            case State.Idle:
                //sr.color = new Color(255f, 255f, 255f, 1f);
                break;
            case State.Approach:
                sr.color = new Color(255f, 255f, 255f, 1f);
                break;
            case State.PouncePrep:
                sr.color = Color.yellow;
                prepareTimer = prepareDuration;
                break;
            case State.Pounce:
                //sr.color = new Color(0, 255f, 0f, 1f);
                break;
            case State.DashPrep:
                sr.color = Color.blue;
                prepareTimer = prepareDuration;
                break;
            case State.Dash:
                //sr.color = new Color(255f, 255f, 0f, 1f);
                break;
        }
    }

    void Idle()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= idleRange)
        {
            SwitchState(State.Approach);
        }
    }

    void Approach()
    {
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

    void PouncePrep()
    {
        CheckFacing();
        prepareTimer -= Time.deltaTime;
        if (prepareTimer <= 0){
            SwitchState(State.Pounce);
        }
    } 

    void Pounce()
    {
        if (rb.velocity.y == 0 && !isJumping){
           rb.velocity = new Vector2(direction * pounceSpeedHorizontal, pounceSpeedVertical);
           fangs.SetActive(true);
           isJumping = true;
        }
        else if (rb.velocity.y == 0 && isJumping){
            isJumping = false;
            fangs.SetActive(false);
            SwitchState(State.Approach);         
        }
    }

    void DashPrep()
    {
        CheckFacing();
        prepareTimer -= Time.deltaTime;
        if(prepareTimer <= 0){
            SwitchState(State.Dash);
        }
    }

    void Dash(){
        if (!dashStart){
            dashStart = true;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health -= 1;
            if(health == 0){
                Destroy(this.gameObject);
            }else if(state == State.Idle){
                SwitchState(State.Approach);
            }
            sr.color = new Color(255f, 0f, 0f, 1f);
            isRed = true;
            damageTimer = damageDuration;
        }
        else if(other.tag == "MeleeWeapon")
        {
            health -= 1;
            if(health == 0) Destroy(this.gameObject);
            sr.color = new Color(255f, 0f, 0f, 1f);
            isRed = true;
            damageTimer = damageDuration;
        }
    }
}