using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameMaster gm;

    public bool checkPointReached = false;
    public GameObject checkpointEnemies;
    public GroundEnemy[] enemies;
    public CritterNest[] nests;

    

    private ReturnToCheckpoint returnPlayer;
    private int arrLength;
    private int critterArrLength;

    private Player player;
    private bool backToStart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        player = GameObject.Find("Player").GetComponent<Player>();
        returnPlayer = player.GetComponent<ReturnToCheckpoint>();
        arrLength = enemies.Length;
        critterArrLength = nests.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (returnPlayer.reset && !backToStart){
            
            resetCheckpoint();

        }
        else if (!returnPlayer.reset && backToStart){
            backToStart = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !checkPointReached){
            gm.setCheckpoint(new Vector3  (other.transform.position.x, other.transform.position.y, other.transform.position.z));
            this.checkPointReached = true;
            
        }
    }

    public void resetCheckpoint(){
        
        
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

        for(int i = 0; i < critterArrLength; i++){
            if(!checkPointReached){
                nests[i].ResetNest();
            }else{
                nests[i].DestroyNest();
            }
        }
        
        
        
    backToStart = true;

    }
}
