using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour
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
            
            thought.SetBubbleText("i dont want to fall off that edge");
            thought.ShowBubbleForSeconds(2);
            
        }
       
    }
}
