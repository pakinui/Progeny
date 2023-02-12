using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceBlock : MonoBehaviour
{
    //to access booleans to see if all objs have been interacted with
    //public HouseInteract house;
    public GameObject block;
    //has pounce enemy been killed
    public bool pounceKilled = false;

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
            if(pounceKilled){
                block.SetActive(false);//could probably destroy it?
            }else{
                //if all three objects havent been interacted
                //with then trigger thought bubble
                bubble.SetBubbleText("i feel like i should explore this area a bit more first");
                bubble.ShowBubbleForSeconds(4);
            }
        }
    }
}
