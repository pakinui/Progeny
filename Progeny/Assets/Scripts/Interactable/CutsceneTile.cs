using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTile : MonoBehaviour
{
    
    public PlayerMove player;
    public GameObject monster;
    public GameObject TopBar;
    public GameObject BottomBar;

    //public GameObject thoughtBubble;

    // set to true once the cutscene has been watched
    public static bool completedCutscene = false;

    private Vector3 tempSpeed;

    



    [SerializeField] private PlayableDirector _timeline;


    void Start(){
        monster.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D tile){
        //Debug.Log("standing on tile");
        if(tile.tag == "Player" && !completedCutscene){
            monster.SetActive(true);
            StartCutscene();
            

        }

     }



    public void StartCutscene(){
        Debug.Log("start cutscene: " + player.rb.transform.position.y);


        //TopBar.SetActive(true);
        BottomBar.SetActive(true);
        //stop player from being able to move
        tempSpeed = player.rb.velocity;
        player.rb.velocity = Vector3.zero;
        //player.player.setMoving(false);
        player.player.setAllowedMovement(false);//stop player from being able to move
        
        _timeline.Play();
        completedCutscene = true;

    }


    public void EndCutscene(){
        //Debug.Log("cutscene finished");
        //thoughtBubble.SetActive(true);
        //TopBar.SetActive(false);
        BottomBar.SetActive(false);
        player.rb.velocity = tempSpeed;
        player.player.setMoving(true);
        player.player.setAllowedMovement(true);//allow player from being able to move
        //thoughtBubble.SetActive(true);
    }

}
