using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameMaster gm;

    public bool checkPointReached = false;
    public GameObject checkpointEnemies;
    public GroundEnemy[] enemies;

    

    private ReturnToCheckpoint returnPlayer;
    private int arrLength;

    private Player player;
    private bool backToStart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        player = GameObject.Find("Player").GetComponent<Player>();
        returnPlayer = player.GetComponent<ReturnToCheckpoint>();
        arrLength = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (returnPlayer.reset == true && !backToStart){
            resetLevel();
            
            //returnPlayer.reset = false;//stop resetting
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !checkPointReached){
            gm.setCheckpoint(other.transform.position);
            this.checkPointReached = true;
            
        }
    }

    public void resetLevel(){
        
        
        for(int i = 0 ; i < arrLength ; i++){
            if(!checkPointReached){
                //if the player has not reached the checkpoint then reset the enemies
                
                enemies[i].resetPosition();
            }else{
                if(enemies[i] != null){
                    enemies[i].DestroyEnemy();
                }
                

            }
        }
        
    backToStart = true;

    }
}
