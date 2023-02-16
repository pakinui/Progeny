using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBlock : MonoBehaviour
{
    //to access booleans to see if all objs have been interacted with
    //public HouseInteract house;
    public GameObject block;
    //have obj been interacted with
    public bool melee = false;

    private Player player;
    private ThoughtBubble bubble;


    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        
    }




    void Update(){
        if(melee){
            bubble.hideBubble();
            Destroy(this.gameObject);
            Destroy(block);
         }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            bubble.SetBubbleText("i think i should explore this area more before i leave.");
            bubble.ShowBubbleForSeconds(3);
            
        }
    }
}
