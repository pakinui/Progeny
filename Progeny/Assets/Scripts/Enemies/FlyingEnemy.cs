using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

public class FlyingEnemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Approach,
        Prepare,
        Attack,
        Wait,
        Return
    }

    public State state = State.Idle;
    // enemy health
    public int health = 1;
    public float damageDuration = 1;
    private float damageTimer;
    private bool isRed = false;

    public float idleRange; // range enemy will swap from idle to approach
    public float approachRange; // range enemy will swap from approach to prepare
    public float returnRange;
    public float speed = 4;
    public float prepareDuration = 0.5f;
    private float prepareTimer;
    public float waitDuration = 1.5f;
    private float waitTimer;

	public float bobRate; // Rate of the 'bob' movement
	public float avgBobScale; // Scale of the 'bob' movement

    public Transform shotPrefab;
    private GameObject player;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float direction = -1;
    private bool facingLeft = true;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
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
            case State.Attack:
                Attack();
                break;
            case State.Wait:
                Wait();
                break;
            case State.Return:
                Return();
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

    void SwitchState(State nextState)
    {
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
            case State.Attack:
                sr.color = new Color(0, 255f, 0f, 1f);
                break;
            case State.Wait:
                sr.color = new Color(0, 255f, 0f, 1f);
                waitTimer = waitDuration;
                break;
            case State.Return:
                sr.color = new Color(0, 0f, 0f, 1f);
                break;
        }
    }

    void Idle()
    {
        if (Math.Abs(player.transform.position.x - transform.position.x) <= idleRange)
        {
            SwitchState(State.Approach);
        }
    }

    void Approach()
    {
        CheckFacing();
        Vector2 velocity = new Vector2(direction * speed, rb.velocity.y);
        rb.velocity = velocity;

        // Change in vertical distance 
		float dy = (Random.Range(avgBobScale - avgBobScale/10f, avgBobScale + avgBobScale/10f)) * Mathf.Sin(bobRate * Time.time);
		// Move the game object on the vertical axis
		transform.Translate(new Vector3(0, dy, 0));

        if (Math.Abs(player.transform.position.x - transform.position.x) <= approachRange)
        {
            rb.velocity = new Vector2(0, 0);
            SwitchState(State.Prepare);
        }
    }

    void Prepare()
    {
        CheckFacing();
        prepareTimer = Timer(prepareTimer);
        if (prepareTimer <= 0){
            SwitchState(State.Attack);
        }
    }

    void Attack()
    {
        // Create a projectile object from 
        // the shot prefab
        Transform shot = Instantiate(shotPrefab);
        // Set the position of the projectile object
        // to the position of the firing game object
        shot.position = transform.position;
        SwitchState(State.Wait);
    }

    void Wait(){
        waitTimer = Timer(waitTimer);
        if (waitTimer <= 0){
            SwitchState(State.Return);
        }
    }

    void Return(){
        if (originalPosition.x > transform.position.x)
        {
            direction = 1;
            if (facingLeft)
            {
                Flip();
            }
        }
        else if (originalPosition.x < transform.position.x)
        {
            direction = -1;
            if (!facingLeft)
            {
                Flip();
            }
        }
        else
        {
            direction = 0;
        }
        Vector2 velocity = new Vector2(direction * speed, rb.velocity.y);
        rb.velocity = velocity;
        if (Math.Abs(originalPosition.x - transform.position.x) <= returnRange)
        {
            rb.velocity = new Vector2(0, 0);
            SwitchState(State.Idle);
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckFacing()
    {
        if (player.transform.position.x > transform.position.x)
        {
            direction = 1;
            if (facingLeft)
            {
                Flip();
            }
        }
        else if (player.transform.position.x < transform.position.x)
        {
            direction = -1;
            if (!facingLeft)
            {
                Flip();
            }
        }
        else
        {
            direction = 0;
        }
    }

    private float Timer(float timer){
        timer -= Time.deltaTime;
        return timer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health -= 1;
            if(health == 0) Destroy(this.gameObject);
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