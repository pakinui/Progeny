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
    private GameObject display;

    private Player player;
    private StoryText story;
    private GroundEnemy enemy;

    private bool pouncePhase = false;
    private bool pouncePause = false;
    private float playerPos;
    private float enemyPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        enemy = pounceEnemy.GetComponent<GroundEnemy>();

        playerPos = player.transform.position.x;
        enemyPos = enemy.transform.position.x;
        //Debug.Log(playerPos);
    }



    // Update is called once per frame
    void Update()
    {
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
            enemy.speed = 3;
            enemy.state = GroundEnemy.State.Pounce;
            pouncePause = false;
            display = Instantiate(displayPrefab, new Vector3(player.transform.position.x, player.transform.position.y+3.5f), new Quaternion(0,0,0,0), this.transform);
        }

        if(enemy.state == GroundEnemy.State.Pounce && !pouncePause){
            pouncePhase = true;
            if (!Input.GetKey("s")){
                Time.timeScale = 0;
            }
            else{
                Time.timeScale = 1;
            }
        }

        if (enemy.state != GroundEnemy.State.Pounce && pouncePhase){
            Destroy(this);
            Destroy(display);
        }
        
    }


    public void StartPounceTutorial(){
        pounceEnemy.SetActive(true);


    }
}
