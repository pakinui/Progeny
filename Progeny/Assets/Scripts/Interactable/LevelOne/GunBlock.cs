using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBlock : MonoBehaviour
{
    //to access booleans to see if all objs have been interacted with
    //public HouseInteract house;
    public GameObject block;
    //have obj been interacted with
    public bool gun = false;

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
            if(gun){
                block.SetActive(false);//could probably destroy it?
            }else{
                //if all three objects havent been interacted
                //with then trigger thought bubble
                bubble.SetBubbleText("there's a monster ahead... i should probably pick up that gun");
                bubble.ShowBubbleForSeconds(4);
            }
        }
    }
}
