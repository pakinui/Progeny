using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : MonoBehaviour
{
    
    //text to pass onto storyText and display
    public TextAsset textFile;


    private bool tutorialCompleted = false;
   
    private Player player;
    private StoryText story;
    private GroundEnemy enemy;
    private ThoughtBubble bubble;

    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        //enemy = GameObject.Find("ShootingEnemy"). GetComponent<GroundEnemy>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        //bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        // textFile = GetComponent<TextAsset>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player" && !tutorialCompleted){
            //trigger tutorial
           
            stopPlayerMovement();
            Debug.Log(story.storyComplete);
            story.PlayStoryText(textFile);
            //bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

        }
    }



    void Update(){

        // if(story.storyComplete && !tutorialCompleted){

        //     bubble.SetBubbleText("hold right to aim, left click to shoot. . .");
        //     //story.textBox.SetActive(false);
        //     bubble.ShowBubble();
            
        // }

        // if(enemy.health == 0 && !tutorialCompleted){
        //     Debug.Log("died");
        //     //story.textBox.SetActive(false);
        //     bubble.hideBubble();
        //     tutorialCompleted = true;
        // }
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
