using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceEnemy : MonoBehaviour
{
    public enum State
    {
        Idle, //does nothing until player is in range
        Approach,//starts to go towards player until in range of attack
        Prepare,//charges attack up
        Pounce//leaps toward player
    }

    public State state = State.Idle;
    public float idleRange; //range needed for idle to end 
    public float approachRange; //range needed for approach to end
    public float speed = 4;
    public float pounceSpeedVertical = 7;
    public float pounceSpeedHorizontal = 10;
    public float prepareDuration = 1.5f; //time to wait before pounce
    private float prepareTimer;
    private GameObject player;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float direction = -1; //negative for left, positive for right
    private bool facingLeft = true; //starts facing left
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
            case State.Prepare:
                Prepare();
                break;
            case State.Pounce:
                Pounce();
                break;
        }
    }

    void SwitchState(State nextState){
        state = nextState;
        switch (nextState)
        {
            case State.Idle:
                sr.color = new Color(255f, 255f, 255f, 1f);
                break;
            case State.Approach:
                sr.color = new Color(255f, 0f, 0f, 1f);
                break;
            case State.Prepare:
                sr.color = new Color(0f, 0f, 255f, 1f);
                prepareTimer = prepareDuration;
                break;
            case State.Pounce:
                sr.color = new Color(0, 255f, 0f, 1f);
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
            SwitchState(State.Prepare);
        }
    }

    void Prepare()
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
           isJumping = true;
        }
        else if (rb.velocity.y == 0 && isJumping){
            isJumping = false;
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
}