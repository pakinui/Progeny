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
    
    void OnEnabled(){
        pushTutorialCompleted = false;
        pushStoryDone = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        //trash = GetComponent<Pushable>();
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

            
            
            
                    
                
            

        }
    }
}
