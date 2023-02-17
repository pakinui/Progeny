using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingCart : MonoBehaviour
{
    
    private Player player;
    private bool contact = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(contact){
            player.setPushing(true);
        }
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
}
