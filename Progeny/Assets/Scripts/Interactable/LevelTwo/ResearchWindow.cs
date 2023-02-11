using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchWindow : MonoBehaviour
{
    
    private Player player;
    private ThoughtBubble thought;
    private bool contact = false; // true when player is in contact with trigger
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = true;
            thought.SetBubbleText("maybe I could climb through this window?");
            thought.ShowBubbleForSeconds(2);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(contact){
            
            if(Input.GetKeyDown("e")){
                Debug.Log("next level");
                //TODO move player to next level
            }
            
        }
       
    }
}
