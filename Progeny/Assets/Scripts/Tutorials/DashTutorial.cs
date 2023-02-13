using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTutorial : MonoBehaviour
{
    
    public TextAsset dashTextFile;
    public GameObject dashEnemy;
    public MeleeTutorial meleeWeapon;

    public GameObject displayPrefab;
    private GameObject display;


    private Player player;
    private StoryText story;
    private GroundEnemy enemy;
    private BoxCollider2D triggerRb;

    private bool currentlyTutorial = true;
    private bool currentlyDashTutorial = true;
    private bool currentlyParryTutorial = false;
    private bool dashPause = false;
    
    private float playerPos;
    private float enemyPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dashEnemy.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        enemy = dashEnemy.GetComponent<GroundEnemy>();
       
    }



    // Update is called once per frame
    void Update()
    {

        if(dashEnemy == null || enemy.health == 0){
           // Destroy(tutorial);
           //Destroy(this.gameObject);
            this.gameObject.SetActive(false);//dont destroy(for checkpoint)
        }
        
        if(currentlyTutorial){
            playerPos = player.transform.position.x;
            enemyPos = enemy.transform.position.x;

            if(currentlyDashTutorial){
                //pause in dash prep phase


                if(enemy.state == GroundEnemy.State.DashPrep && !dashPause){
                    player.stopPlayerMovement();
                    story.PlayStoryText(dashTextFile);
                    enemy.speed = 0;
                    enemy.dashSpeed = 0;
                    dashPause = true;
                }

                //keep in dash state while text plays
                if(enemy.state != GroundEnemy.State.DashPrep && dashPause){
                    enemy.state = GroundEnemy.State.DashPrep;
                }

                //story text finished, enemy dashes
                if(dashPause && story.storyComplete){
                    enemy.speed = 5;
                    enemy.dashSpeed = 9;
                    dashPause = false;
                    enemy.state = GroundEnemy.State.Dash;
                    display = Instantiate(displayPrefab, new Vector3(player.transform.position.x, player.transform.position.y+3.5f), new Quaternion(0,0,0,0), this.transform);
                    currentlyDashTutorial = false;
                    currentlyParryTutorial = true;
                }
            }

            if(currentlyParryTutorial){
                Debug.Log(playerPos + 1.0f + enemyPos);
                if((playerPos + 2.0f) > enemyPos){

                    if(!Input.GetMouseButtonDown(1)){
                        Time.timeScale = 0;
                    }else{
                        
                        Time.timeScale = 1;
                        currentlyParryTutorial = false;
                        Destroy(display);
                    }
                }
            }
        
        
        }
    }


    public void StartDashTutorial(){
        dashEnemy.SetActive(true);
        enemy.state = GroundEnemy.State.Approach;
        currentlyDashTutorial = true;

    }
}
