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
    public AudioClip cutsceneSound;

    private AudioSource audioSource;
    private CameraController cameraController;

    //public GameObject thoughtBubble;

    // set to true once the cutscene has been watched
    public static bool completedCutscene = false;

    private Vector3 tempSpeed;

    



    [SerializeField] private PlayableDirector _timeline;


    void Start(){
        monster.SetActive(false);
        audioSource = player.GetComponent<AudioSource>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
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
        cameraController.Mute(true, 1f, cutsceneSound.length);
        audioSource.PlayOneShot(cutsceneSound, 1);

        //TopBar.SetActive(true);
        //BottomBar.SetActive(true);
        //stop player from being able to move
        tempSpeed = player.rb.velocity;
        player.rb.velocity = Vector3.zero;
        //player.player.setMoving(false);
        player.player.setAllowedMovement(false);//stop player from being able to move
        
        _timeline.Play();
        completedCutscene = true;
    }


    public void EndCutscene(){
        cameraController.Mute(false, 1.5f);
        //Debug.Log("cutscene finished");
        //thoughtBubble.SetActive(true);
        //TopBar.SetActive(false);
        //BottomBar.SetActive(false);
        player.rb.velocity = tempSpeed;
        player.player.setMoving(true);
        player.player.setAllowedMovement(true);//allow player from being able to move
        //thoughtBubble.SetActive(true);
    }

}
