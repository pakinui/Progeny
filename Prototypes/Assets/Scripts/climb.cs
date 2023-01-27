using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climb : MonoBehaviour
{
    
    private bool c = false;
    public GameObject ledge;
    public PlayerMovement player;
    
    private Rigidbody2D ledgeRb;
    private float ledgeX;
    private float ledgeY;

    private float animationTime = 3;
    private SpriteRenderer sprite;

    //boolean to tell if character is currently climbing
    private bool currClimbing;

    void Awake(){
        sprite = GetComponent<SpriteRenderer>();
        ledgeRb = GetComponent<Rigidbody2D>();
        ledgeX = ledgeRb.transform.position.x;
        ledgeY = ledgeRb.transform.position.y;

        Debug.Log("x: " + ledgeX);
        Debug.Log("y: " + ledgeY);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            c = true;
            Debug.Log("start climb");
        }
        
        
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            c = false;
            Debug.Log("stop climb: " + c);

            
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && c){
            
            
            Debug.Log("to climb: " + c);

            if(!currClimbing){
                //start climbing
                currClimbing = true;
                
                StartClimbing();
            }
            

            
            
            

        }
        //square x = 6.264 y = -2.279

        //x = 5.504976 y = -2.704977
        //after
        //x = 11.76897 y = -6.294999
        //want it to be x = 6.41 y = 0.72 ish
    }

    void StartClimbing(){
        //wait for animation
        Debug.Log("starting to climb");
            //then move player to top of ledge
            // var renderer = gameObject.GetComponent<Renderer>();
            // int width = renderer.bounds.size.x;
                
                sprite.color = new Color (215, 63, 63, 255);
            
           
                float x = (player.render.bounds.size.x/2.0f) + ledgeX;
                float y = (player.render.bounds.size.y/2.0f) + ledgeY;

                //Debug.Log(player.rb.transform.position.x + ", " + ledgeX +  ", " + x);
                //Debug.Log(player.rb.transform.position.y +  ", " + ledgeY +  ", " + y);
                //x = 

            // Debug.Log(player.render.bounds.size.x);
                //sprite.color = new Color (255, 255, 255, 255);
                player.rb.transform.position = new Vector3(x, y, player.rb.transform.position.z);

                currClimbing = false;
                Debug.Log("climbing stoped");
    }
}
