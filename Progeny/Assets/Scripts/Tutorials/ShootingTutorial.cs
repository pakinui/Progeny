using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : MonoBehaviour
{
    
    //text to pass onto storyText and display
    public TextAsset textFile;
    public GroundEnemy enemy;
    public GroundEnemy secondEnemy;

    private bool tutorialCompleted = false;
   
    private Player player;
    private StoryText story;
    
    private ThoughtBubble bubble;

    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player" && !tutorialCompleted){
            //trigger tutorial
            player.stopPlayerMovement();
            story.PlayStoryText(textFile);;

        }
    }

    void Update(){

        //when storytext completed show thought
        if(story.storyComplete && !tutorialCompleted){
            bubble.SetBubbleText("hold right to aim, left click to shoot. . .");
            bubble.ShowBubble();
        }

        //when enemy is killed get rid of thought
        if(enemy.health == 0 && !tutorialCompleted){
            bubble.hideBubble();
            tutorialCompleted = true;
        }

        if(secondEnemy.health < 3){
            bubble.SetBubbleText("this one can move!");
            bubble.ShowBubbleForSeconds(2.0f);
        }

        
    }

}
