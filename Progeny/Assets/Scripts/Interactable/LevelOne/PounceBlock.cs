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
        if(pounceKilled){
            bubble.hideBubble();
            Destroy(block);
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            bubble.SetBubbleText("i should kill this pouncer before i go ahead...");
            bubble.ShowBubbleForSeconds(4);
        }
    }

 }
