using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItInteract : MonoBehaviour
{
    
    public GameObject bigPostIt; 
    public GameObject display; // E to display
    

    private Player player;
    private StoryText story;
    private ThoughtBubble bubble;
    private bool contact = false;
    private bool imgOpen = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

    }

    // Update is called once per frame
    void Update()
    {
        //if player presses e and img is not open yet
        if(contact && !imgOpen){
            if(Input.GetKeyDown("e")){
                player.stopPlayerMovement();
                bigPostIt.SetActive(true);
                imgOpen = true;
                display.SetActive(false);
                
            }
        }else if(contact && imgOpen){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartThought();
            }
        }
    }

    private void StartThought(){
        bigPostIt.SetActive(false);
        imgOpen = false;
        player.startPlayerMovement();
        bubble.SetBubbleText("Maybe I could trap it at Bates Farm?\n I just need to find something that will hurt it.");
        bubble.ShowBubbleForSeconds(3);
    }

    // when player enters box collider and is close 
    // enough to interact
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = true;
            display.SetActive(true);
            
        }
    }

    // when player exits box collider and is no longer
    // close enough to interact
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = false;
            display.SetActive(false);
        }
    }
}

