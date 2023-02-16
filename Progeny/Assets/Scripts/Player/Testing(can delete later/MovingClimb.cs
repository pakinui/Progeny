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
    private PlayerAnimationController ac;

    private float playerWidth;
    private float playerHeight;
    
    private float currPlayerHeight;
    private float currPlayerWeight;
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
                box.mass = 100;
                
                currClimbing = true;
                player.setPushing(false);
                
                player.stopPlayerMovement();
                player.setClimbing(true);

                // stop box from moving
                //teleport character
                if (gameObject.tag == "RightClimb")
                {
                    x = boxX -0.1f;
                    
                    if(!shortClimb){
                        player.transform.position = new Vector3((x + 0.85f), player.transform.position.y ,player.transform.position.z);
                    }
                   
                }
                else if (gameObject.tag == "LeftClimb")
                {
                    x = boxX + 0.1f;
                    if(!shortClimb){
                        player.transform.position = new Vector3((x - 0.95f), player.transform.position.y ,player.transform.position.z);
                    }
                    
                    
                }
                y = (player.playerHeight/2f) + (boxY+0.1f);

                player.climbPosition = new Vector3(x, y, player.transform.position.z);
                //1.83
                

                if(shortClimb){
                    ac.anim.SetTrigger("shortClimb");
                }else{
                    ac.anim.SetTrigger("climbing");
                }
            }
        }
        boxX = transform.position.x;
        boxY = transform.position.y;
    }
}

