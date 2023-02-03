using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTutorial : MonoBehaviour
{
    
    public TextAsset dashTextFile;
    public TextAsset smashTextFile;
    public GameObject dashEnemy;
    public MeleeTutorial meleeWeapon;
    public GameObject smashTrigger;


    private Player player;
    private StoryText story;
    private GroundEnemy enemy;
    private BoxCollider2D triggerRb;


    private bool currentlyDashTutorial = false;
    private bool dashPause = false;
    private bool smashPause = false;
    private float playerPos;
    private float enemyPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dashEnemy.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        enemy = dashEnemy.GetComponent<GroundEnemy>();
        triggerRb = smashTrigger.GetComponent<BoxCollider2D>();
        //enemy.state = GroundEnemy.State.DashPrep;
        Debug.Log(enemy.state);
       
    }



    // Update is called once per frame
    void Update()
    {
        
        if(currentlyDashTutorial){
            //pause in dash prep phase
            playerPos = player.transform.position.x;
            enemyPos = enemy.transform.position.x;
            if(enemy.state == GroundEnemy.State.DashPrep && !dashPause){
                player.stopPlayerMovement();
                story.PlayStoryText(dashTextFile);
                enemy.speed = 0;
                enemy.dashSpeed = 0;
                dashPause = true;
            }

            if(dashPause && story.storyComplete){
                enemy.speed = 3;
                enemy.dashSpeed = 9;

            }
                Debug.Log(playerPos+ 3.0f);
                Debug.Log(enemyPos);
            if((playerPos + 3.0f) > enemyPos && dashPause){
                enemy.speed = 0;
                enemy.dashSpeed = 0;
                player.stopPlayerMovement();
                story.PlayStoryText(smashTextFile);
            }
        }
        
        
        
    }


    public void StartDashTutorial(){
        dashEnemy.SetActive(true);
        enemy.state = GroundEnemy.State.Approach;
        currentlyDashTutorial = true;

    }
}
