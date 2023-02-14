using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClimb : MonoBehaviour
{
    //boolean, true when player is close enough 
    // (in contact with box) to climb
    public bool contact = false;

    //boolean to check if climb trigger should 
    //do short or normal climb animation
    public bool shortClimb = false;
    public Rigidbody2D box; // corner of climbing box
    public bool pushable = false; // is this obj pushable
    public bool climbable = false;

    private PlayerMove playerMove;
    private Player player;


    private float boxX;
    private float boxY;

    //player coords
    private float x;
    private float y;

    //push obj weight
    private float tempMass;

    // time left for climbing animation to finish
    private float climbTime = 1.1f;


    //private float animationTime = 3;

    public PlayerAnimation playerAnimation;
    private PlayerAnimationController ac;

    //boolean to see if character is currently climbing
    private bool currClimbing;

    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Player>();
        playerMove = player.GetComponent<PlayerMove>();
        boxX = transform.position.x;
        boxY = transform.position.y;
        tempMass = box.mass;
        // assigning references
        ac = player.GetComponent<PlayerAnimationController>();

    }

    // when player enters box collider and is close 
    // enough to climb
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            contact = true;
            if (pushable)
            {
                player.setPushing(true);
                box.mass = 30;
            }

        }
    }

    // when player exits box collider and is no longer
    // close enough to climb
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            currClimbing = false;
            contact = false;
            if (pushable)
            {
                player.setPushing(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        float userInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && contact)
        {
            if (!currClimbing && climbable)
            {
                currClimbing = true;
                player.setPushing(false);
                ac.anim.SetTrigger("climbing");
                //player.stopPlayerMovement();
                player.setClimbing(true);

                // stop box from moving

                box.mass = 100;

                //wait for animation TODO
                Debug.Log("player :" + player.transform.position.x + " || " + player.transform.position.y);
                //teleport character
                if (gameObject.tag == "RightClimb")
                {
                    x = player.transform.position.x - (playerMove.render.bounds.size.x /2.0f);
                   
                }
                else if (gameObject.tag == "LeftClimb")
                {
                    x = player.transform.position.x + (playerMove.render.bounds.size.x /2.0f);
                    
                }
                y = (playerMove.render.bounds.size.y / 2.0f) + (boxY+0.1f);

                player.climbPosition = new Vector3(x, y, player.transform.position.z);
        
                //player.transform.position = new Vector3(x, y, player.transform.position.z);
                //player.setClimbing(false);
            }else if (currClimbing){
                if(climbTime <= 0) {
                    player.ClimbAction();
                    currClimbing = false;
                }else{
                    Debug.Log("time: " + climbTime);
                    climbTime -= Time.deltaTime;
                }
            }
            
            
        }
        else if (userInput != 0f)
        {
            //player is pushing left or right buttons
            //start pushing animation


        }

        // if (currClimbing && climbingTimeRemaining < 0)
        // {

        //     player.transform.position = new Vector3(x, y, player.transform.position.z);
        //     currClimbing = false;
        //     box.mass = tempMass;//return to normal mass
        //     player.setClimbing(false);
        //     climbingTimeRemaining = 1.2f;


        // }
        // else if (currClimbing)
        // {

        //     climbingTimeRemaining -= Time.deltaTime;

            
        // }

        boxX = transform.position.x;
        boxY = transform.position.y;
    }


}

