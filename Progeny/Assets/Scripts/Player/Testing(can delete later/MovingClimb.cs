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
    private float climbingTimeRemaining = 1.2f;


    //private float animationTime = 3;

    public PlayerAnimation playerAnimation;

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
            }

        }
    }

    // when player exits box collider and is no longer
    // close enough to climb
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
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
                player.stopPlayerMovement();
                player.setClimbing(true);

                // stop box from moving

                box.mass = 100;

                //wait for animation TODO
                //Debug.Log("player :" + player.transform.position.x + " || " + player.transform.position.y);
                //teleport character
                if (gameObject.tag == "RightClimb")
                {
                    x = player.transform.position.x - (playerMove.render.bounds.size.x / 3.0f);
                    // x = player.transform.position.x - (playerMove.render.bounds.size.x/4.0f);

                    //climbing sprites diff size so need to set new starting coords

                    //var tempX = player.transform.position.x + playerMove.render.bounds.size.x;
                    // var tempY = player.transform.position.y + playerMove.render.bounds.size.y;
                    // //tempY -= 288;
                    // player.transform.position = new Vector3(player.transform.position.x, tempY, player.transform.position.z);
                    //Debug.Log(" new player :" + player.transform.position.x + " || " + player.transform.position.y);

                }
                else if (gameObject.tag == "LeftClimb")
                {
                    x = player.transform.position.x + (playerMove.render.bounds.size.x / 3.0f);
                    // x = (playerMove.render.bounds.size.x/2.0f) + boxX;  
                    //var tempX = player.transform.position.x + playerMove.render.bounds.size.x;
                    // var tempY = player.transform.position.y + playerMove.render.bounds.size.y;
                    // //tempY -= 288;
                    // player.transform.position = new Vector3(player.transform.position.x, tempY, player.transform.position.z);
                    //Debug.Log(" new 2player :" + player.transform.position.x + " || " + player.transform.position.y);

                }
                y = (playerMove.render.bounds.size.y / 2.0f) + boxY;
                //Debug.Log(x + " || " + y);
                //var tempY = player.transform.position.y + 0.1f;
                //tempY -= 288;
                //var tempY = player.transform.position.y + (playerMove.render.bounds.size.y/2);
                //player.transform.position = new Vector3(player.transform.position.x, tempY, player.transform.position.z);

            }
        }
        else if (userInput != 0f)
        {
            //player is pushing left or right buttons
            //start pushing animation


        }

        if (currClimbing && climbingTimeRemaining < 0)
        {

            player.transform.position = new Vector3(x, y, player.transform.position.z);
            currClimbing = false;
            box.mass = tempMass;//return to normal mass
                                //player.SetMovement(true);

            //finish climbing animation
            player.setClimbing(false);
            player.startPlayerMovement();
            //Debug.Log("time remaining: " + climbingTimeRemaining);
            climbingTimeRemaining = 1.2f;


        }
        else if (currClimbing)
        {

            climbingTimeRemaining -= Time.deltaTime;

            //Debug.Log(playerMove.render.bounds.size.y + " |1| " + y);
        }

        boxX = transform.position.x;
        boxY = transform.position.y;
    }


}

