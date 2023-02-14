using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingTutorial : MonoBehaviour
{
    
    public TextAsset textFile;
    public Pushable trash;


    //is pushing tutorial completed
    private bool pushTutorialCompleted = false;
    private bool pushStoryDone = false;// has player read slides
    
    private bool beenTriggered = false;//has this tile been triggered

    private Player player;
    private StoryText story;
    private ThoughtBubble bubble;
    private Rigidbody2D rb;

    //if player has not climbed the semi truck in 6 sec
    //a thought bubble will help them
    private float timeRemaining = 6;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D coll){

        if(coll.tag == "Player" && !pushTutorialCompleted && !pushStoryDone){
            beenTriggered = true;

            //trigger tutorial
            player.stopPlayerMovement();
            story.PlayStoryText(textFile);
            trash.SetPushable(true);//allows trash to be pushable
            
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(story.storyComplete && beenTriggered){
            pushStoryDone = true;
            //if finished story text            
            if(!pushTutorialCompleted){
                //bubble until player climbs
                
                bubble.SetBubbleText("could i push that trash bin?");
                bubble.ShowBubbleForSeconds(3);
                pushTutorialCompleted = true;
                
                
            }

            //countdown time until hint
            if(pushTutorialCompleted){
                
                if(timeRemaining > 0){
                    timeRemaining -= Time.deltaTime;
                }else if(timeRemaining == 0){
                    //show hint
                    bubble.SetBubbleText("maybe i could push the rubbish bin towards the semitruck and then climb it?");
                    bubble.ShowBubbleForSeconds(3);
                    timeRemaining -= Time.deltaTime;
                }else{
                    //for another hint if needed
                    timeRemaining = 10;
                }
            }

            if(player.transform.position.x > rb.transform.position.x){
               //if player has climbed on the semi
               Destroy(this.gameObject);
            }

            
            
            
                    
                
            

        }
    }
}
