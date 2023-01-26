using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTile : MonoBehaviour
{
    
    public PlayerMovement player;

    public GameObject thoughtBubble;

    // set to true once the cutscene has been watched
    public static bool completedCutscene = false;

    private Vector3 tempSpeed;

    [SerializeField] private PlayableDirector _timeline;


     void OnTriggerEnter2D(Collider2D tile){
        if(tile.tag == "Player" && !completedCutscene){
            
            StartCutscene();
            

        }

     }



    public void StartCutscene(){
        Debug.Log("start cutscene");



        //stop player from being able to move
        tempSpeed = player.rb.velocity;
        player.rb.velocity = Vector3.zero;
        player.setAbilityToMove(false);
        
        _timeline.Play();
        completedCutscene = true;

    }


    public void EndCutscene(){
        Debug.Log("cutscene finished");
        //thoughtBubble.SetActive(true);
        player.rb.velocity = tempSpeed;
        player.setAbilityToMove(true);
    }

}
