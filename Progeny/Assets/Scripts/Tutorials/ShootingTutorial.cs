using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : MonoBehaviour
{
    
    //text to pass onto storyText and display
    public TextAsset shootingTextFile;
    public GroundEnemy enemy;
    public GroundEnemy secondEnemy;
    public GameObject melee;

    private bool tutorialCompleted = false;
   
    private Player player;
    private StoryText story;
    
    private ThoughtBubble bubble;
    private Rigidbody2D enemyRb;
    private Rigidbody2D meleeRb;

    private Vector3 enemyDeathSpot;
    //private bool enemyOneDead = false;
    private bool enemyTwoDead = false;
    private bool meleeDropped = false;


    void Start(){

        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

        enemyRb = secondEnemy.GetComponent<Rigidbody2D>();
        meleeRb = melee.GetComponent<Rigidbody2D>();
        melee.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player" && !tutorialCompleted){
            //trigger tutorial
            player.stopPlayerMovement();
            story.PlayStoryText(shootingTextFile);

        }
    }

    void Update(){

        
        
            //when storytext completed show thought
            if( story.storyComplete && !tutorialCompleted){
            
            bubble.SetBubbleText("hold right to aim, left click to shoot. . .");
            bubble.ShowBubble();
            enemy.speed = 1.5f;
            enemy.dashSpeed = 1.5f;
            

            }
            //first enemy died
            if(enemy == null && !tutorialCompleted){
                bubble.hideBubble();
                tutorialCompleted = true;
                story.storyComplete = false;
                
                bubble.SetBubbleText("this one is faster!");
                bubble.ShowBubbleForSeconds(2.0f);
            }
            if (enemy.health == 1) secondEnemy.idleRange = 30;

            //when enemy is killed get rid of thought
            if(enemy.health <= 1 && !tutorialCompleted){
                
                
            }
            if(secondEnemy.health > 0){
                //update melee weapon drop position
                enemyDeathSpot = enemyRb.position;

            }
            if(secondEnemy == null && !meleeDropped){
                enemyTwoDead = true;
                meleeRb.transform.position = enemyDeathSpot;
                melee.SetActive(true);
                bubble.SetBubbleText("whats that?");
                bubble.ShowBubbleForSeconds(2);
                meleeDropped = true;
            } 

            if(enemyTwoDead && meleeDropped){
                //drop melee weapon
                
                
                story.storyComplete = false;//reset story
                Destroy(this.gameObject);
                
            }
        
        

        
    }

}
