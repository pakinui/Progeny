using System.Diagnostics;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTutorial : MonoBehaviour
{
    
    public TextAsset dashTextFile;
    public GameObject dashEnemy;
    public MeleeTutorial meleeWeapon;
    public GameObject smashTrigger;


    private Player player;
    private StoryText story;
    private GroundEnemy enemy;
    private BoxCollider2D triggerRb;

    private bool dashPause = false;
    private float playerPos;
    private float enemyPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        enemy = dashEnemy.GetComponent<GroundEnemy>();
        triggerRb = smashTrigger.GetComponent<BoxCollider2D>();

        playerPos = player.transform.position.x;
        enemyPos = enemy.transform.position.x;
        //Debug.Log(playerPos);
    }



    // Update is called once per frame
    void Update()
    {
        //pause in dash prep phase
        if(enemy.state == GroundEnemy.State.DashPrep && !dashPause){
            
            dashPause = true;
            player.stopPlayerMovement();
            story.PlayStoryText(dashTextFile);
        }

        //keep in dashprep phase while story text plays
        if(enemy.state == GroundEnemy.State.DashPrep && dashPause){
            //enemy.speed = 0;
            enemy.state = GroundEnemy.State.DashPrep;
        }

        if(story.storyComplete && dashPause){
            enemy.speed = 3;
            enemy.state = GroundEnemy.State.Dash;
            dashPause = false;
        }

        if(enemy.state == GroundEnemy.State.Dash && !dashPause){
            // triggerRb.OnTriggerEnter2D(Collider2D coll){
            //     if(coll.tag == "Player"){
            //         Debug.Log("hit");
            //     }
            // };
            
        }
        
    }


    public void StartDashTutorial(){
        dashEnemy.SetActive(true);


    }
}
