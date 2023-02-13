using System.Diagnostics;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceTutorial : MonoBehaviour
{
    
    public TextAsset pounceTextFile;
    public GameObject pounceEnemy;
    public GameObject displayPrefab;
    public PounceBlock block;
    private GameObject display;
    
    private Player player;
    private StoryText story;
    private GroundEnemy enemy;
    private Rigidbody2D enemyRb;

    private bool pouncePhase = false;
    private bool pouncePause = false;
    private bool tutorialActive = true;
    private float playerPos;
    private float enemyPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        enemy = pounceEnemy.GetComponent<GroundEnemy>();
        enemyRb = enemy.GetComponent<Rigidbody2D>();
        playerPos = player.transform.position.x;
        enemyPos = enemy.transform.position.x;
        //Debug.Log(playerPos);
    }



    // Update is called once per frame
    void Update()
    {   
        if (tutorialActive){
            //pause in dash prep phase
            if(enemy.state == GroundEnemy.State.PouncePrep && !pouncePause){
                
                pouncePause = true;
                player.stopPlayerMovement();
                story.PlayStoryText(pounceTextFile);
            }

            //keep in dashprep phase while story text plays
            if(enemy.state != GroundEnemy.State.PouncePrep && pouncePause){
                enemy.state = GroundEnemy.State.PouncePrep;
            }

            if(story.storyComplete && pouncePause){
                enemy.speed = 5;
                enemy.state = GroundEnemy.State.Pounce;
                pouncePause = false;
                display = Instantiate(displayPrefab, new Vector3(player.transform.position.x+0.15f, player.transform.position.y-1.4f), new Quaternion(0,0,0,0), this.transform);
                SpriteRenderer rend = display.GetComponent<SpriteRenderer>();
                rend.sortingOrder = 3;
            }

             if (enemyRb.velocity.y <= 0 && pouncePhase){
                Time.timeScale = 1;
                tutorialActive = false;
                Destroy(display);
            }

            if(enemy.state == GroundEnemy.State.Pounce && !pouncePause && tutorialActive){
                pouncePhase = true;
                if (!Input.GetKey("s")){
                    display.SetActive(true);
                    Time.timeScale = 0;
                }
                else{
                    display.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
        if (enemy.health == 0){
            Time.timeScale = 1;
            block.pounceKilled = true;
            Destroy(this);
        }
        
    }


    public void StartPounceTutorial(){
        pounceEnemy.SetActive(true);


    }
}
