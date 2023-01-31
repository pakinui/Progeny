using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClimb : MonoBehaviour
{
    //boolean, true when player is close enough 
    // (in contact with box) to climb
    private bool contact = false;

    public PlayerMove player;
    public Rigidbody2D box; // corner of climbing box
    
    private float boxX;
    private float boxY;

    private float animationTime = 3;

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
        }
    }

    // when player exits box collider and is no longer
    // close enough to climb
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && contact){
            if(!currClimbing){
                currClimbing = true; 
                StartClimbing();
            }
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

            Debug.Log("right climg : x= " + x + ", y= " + y);

            player.rb.transform.position = new Vector3(x, y, player.rb.transform.position.z);
        }else if (gameObject.tag == "LeftClimb"){
            float x = (player.render.bounds.size.x/2.0f) + boxX;
            float y = (player.render.bounds.size.y/2.0f) + boxY;
            Debug.Log("left climb : x= " + x + ", y= " + y);
            player.rb.transform.position = new Vector3(x, y, player.rb.transform.position.z);
        }

        currClimbing = false;
        box.mass = tempMass;//return to normal mass
        //player.SetMovement(true);
    }
}
