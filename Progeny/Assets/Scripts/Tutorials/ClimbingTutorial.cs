using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingTutorial : MonoBehaviour
{

    public TextAsset textFile;
    public MovingClimb leftTrigger;
    
    
    //is climbing tutorial completed
    private bool climbTutorialCompleted = false;
    private bool climbStoryDone = false;// has player read slides
    private bool beenTriggered = false;//has this tile been triggered

    private Player player;
    private StoryText story;
    private ThoughtBubble bubble;
    private Rigidbody2D rb;

    //player current position to check whether they did the climb
    private float startingPosition;
    private float afterPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        rb = player.GetComponent<Rigidbody2D>();
        leftTrigger.GetComponent<MovingClimb>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player" && !climbTutorialCompleted && !climbStoryDone){
            //trigger tutorial
            beenTriggered = true;
            player.stopPlayerMovement();
            story.PlayStoryText(textFile);
            //storyTextDone = true;
            
            startingPosition = rb.transform.position.y;//to check if player climbs
            //Debug.Log(startingPosition);
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(story.storyComplete && beenTriggered){
            climbStoryDone = true;
            //if finished story text            
            if(!climbTutorialCompleted){
                //bubble until player climbs
                bubble.SetBubbleText("so if i walk up to it and press space. . .");
                bubble.ShowBubble();
            }

            if(Input.GetKeyDown(KeyCode.Space) && leftTrigger.contact && !climbTutorialCompleted){
                // if(player.rb.transform.position.y)
                
                
                    climbTutorialCompleted = true;
                    bubble.hideBubble();
                    bubble.SetBubbleText("nice");
                    bubble.ShowBubbleForSeconds(2);
                    Destroy(this.gameObject);
                    
        
            }

        }
        



    }

  
}
