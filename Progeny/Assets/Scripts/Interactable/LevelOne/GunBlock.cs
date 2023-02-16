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
        if(gun){
            bubble.hideBubble();
            Destroy(this.gameObject);
            Destroy(block);
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            bubble.SetBubbleText("there's a monster ahead... i should probably pick up that gun");
            bubble.ShowBubbleForSeconds(4);
        }
    }
 }

