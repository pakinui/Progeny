using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchWindow : MonoBehaviour
{
    
    public GameObject display;
    private GameMaster gm;
    private Player player;
    private ThoughtBubble thought;
    private bool contact = false; // true when player is in contact with trigger
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            display.SetActive(true);
            contact = true;
            thought.SetBubbleText("maybe i could climb through this window?");
            thought.ShowBubbleForSeconds(2);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            display.SetActive(false);
            contact = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(contact){
            
            if(Input.GetKeyDown("e")){
                Destroy(display);
               gm.NextLevel("LevelThree");
            }
            
        }
       
    }
}
