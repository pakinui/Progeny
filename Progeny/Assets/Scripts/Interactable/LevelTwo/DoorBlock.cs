using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : MonoBehaviour
{
    //to access booleans to see if all objs have been interacted with
    //public HouseInteract house;
    public GameObject block;

    //have obj been interacted with
    public bool photo = false;
    public bool letter = false;
    public bool calendar = false;

    private Player player;
    private ThoughtBubble bubble;


    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        
    }




    void Update(){


    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            //if all three objs have been interated with
            //then remove door block
            if(photo && letter && calendar){
                block.SetActive(false);//could probably destroy it?
            }else{
                //if all three objects havent been interacted
                //with then trigger thought bubble
                bubble.SetBubbleText("there might be more some more clues to where my family is that I haven't found yet.");
                bubble.ShowBubbleForSeconds(2);
            }
        }
    }
}
