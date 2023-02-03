using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClimb : MonoBehaviour
{
    //boolean, true when player is close enough 
    // (in contact with box) to climb
    public bool contact = false;

    public PlayerMove player;
    public Rigidbody2D box; // corner of climbing box
    public bool pushable = false; // is this obj pushable
    
    private float boxX;
    private float boxY;
    

    //private float animationTime = 3;

    public PlayerAnimation playerAnimation;

    //boolean to see if character is currently climbing
    private bool currClimbing;

    void Start(){
        boxX = transform.position.x;
        boxY = transform.position.y;
        // assigning references
       
    }

    // when player enters box collider and is close 
    // enough to climb
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = true;
            if(pushable){
                player.player.setPushing(true);
            }
            
        }
    }

    // when player exits box collider and is no longer
    // close enough to climb
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = false;
            if(pushable){
                player.player.setPushing(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        float userInput = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && contact){
            if(!currClimbing){
                currClimbing = true; 
                StartClimbing();
            }
        }else if(userInput != 0f){
            //player is pushing left or right buttons
            //start pushing animation


        }   
        boxX = transform.position.x;
        boxY = transform.position.y;
    }

    void StartClimbing(){

        //player.SetMovement(false);
        
        // stop box from moving
        var tempMass = box.mass;
        box.mass = 100;

        //wait for animation TODO
        
        //teleport character
        if(gameObject.tag == "RightClimb"){
           
            float x = player.rb.transform.position.x - (player.render.bounds.size.x/2.0f);
            float y = (player.render.bounds.size.y/2.0f) + boxY;

            //Debug.Log("right climg : x= " + x + ", y= " + y);

            player.rb.transform.position = new Vector3(x, y, player.rb.transform.position.z);
        }else if (gameObject.tag == "LeftClimb"){
            float x = (player.render.bounds.size.x/2.0f) + boxX;
            float y = (player.render.bounds.size.y/2.0f) + boxY;
           // Debug.Log("left climb : x= " + x + ", y= " + y);
            player.rb.transform.position = new Vector3(x, y, player.rb.transform.position.z);
        }

        currClimbing = false;
        box.mass = tempMass;//return to normal mass
        //player.SetMovement(true);
    }
}

