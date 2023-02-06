using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPuddle : MonoBehaviour
{
    
    private Player player;
    private ThoughtBubble thought;
    private Rigidbody2D puddleRb;
    private Rigidbody2D playerRb;
    private float playerPos;
    private float PuddlePos;

    private bool bouncing = false;
    private float timeRemaining = 0.3f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        puddleRb = GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<PlayerMove>().GetComponent<Rigidbody2D>();
    }



    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            thought.SetBubbleText("that water is electric, i shouldnt touch it");
            thought.ShowBubbleForSeconds(2);

            //decrease health
            player.SetCurrentHealth(player.GetCurrentHealth()-5);

            //stop player from being controlled
            player.setAllowedMovement(false);
            bouncing = true;

            //set player red
            player.setRed(true);

            //move player back from puddle
           if(player.transform.position.x < puddleRb.transform.position.x){
            playerRb.velocity = new Vector2((playerRb.transform.position.x - puddleRb.position.x) * 1.5f , playerRb.velocity.y);
           }else{
            playerRb.velocity = new Vector2((puddleRb.position.x - playerRb.transform.position.x) * -1.5f , playerRb.velocity.y);
           }
           
        // }else if(coll.tag == "Enemy"){
        // //     playerRb = coll.GetComponent<Rigidbody2D>();
        // //     //make enemy hurt by water too
        // //     if(playerRb.transform.position.x < puddleRb.transform.position.x){
        // //     playerRb.velocity = new Vector2((playerRb.transform.position.x - puddleRb.position.x) * 0.5f , playerRb.velocity.y);
        // //    }else{
        // //     playerRb.velocity = new Vector2((puddleRb.position.x - playerRb.transform.position.x) * -0.5f , playerRb.velocity.y);
        //    }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(bouncing){
            if(timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }else{
                
                player.setAllowedMovement(true);
                bouncing = false;
            }
        }
    }
}
