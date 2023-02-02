using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTiles : MonoBehaviour
{
    
    /**
        0 = shooting tut
        1 = climbing tut
        2 = pouncing/crouching tut
    */
    public int targetTutorial;

    //public PounceEnemy enemy;

    public TextAsset textFile;

    //private booleans to check if tutorials have been activated
    //or is it better to just delete the obj after the tutorial is complete??
    private bool[] tutorialsCompleted = new bool[3]{false, false, false};
    

    

     private Player player;
     private StoryText story;
     private PounceEnemy enemy;
     // private StoryText story;
    private ThoughtBubble bubble;

    void Start(){
         player = GameObject.Find("Player").GetComponent<Player>();
         enemy = GameObject.Find("ShootingEnemy"). GetComponent<PounceEnemy>();
        story = gameObject.GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        // textFile = GetComponent<TextAsset>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            //trigger tutorial
            if(targetTutorial == 0 && !tutorialsCompleted[0]){
                stopPlayerMovement();
                story.PlayStoryText(textFile);
                bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

            }

            //else tutorial has been done already

        }
    }



    void Update(){

        if(story.storyComplete && !tutorialsCompleted[0]){

            bubble.SetBubbleText("story.theText.text");
            //story.textBox.SetActive(false);
            bubble.ShowBubble();
            
        }

        if(enemy.health == 0 && !tutorialsCompleted[0]){
            Debug.Log("died");
            //story.textBox.SetActive(false);
            bubble.hideBubble();
            tutorialsCompleted[0] = true;
        }
    }



    void ShootingTutorial(){
        
        
    }

    


    void stopPlayerMovement(){
        player.setAllowedMovement(false);
        player.setMoving(false);
    }

    
    void startPlayerMovement(){
        player.setAllowedMovement(true);
        player.setMoving(true);
    }
}
