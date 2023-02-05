using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPuddle : MonoBehaviour
{
    
    private Player player;
    private ThoughtBubble thought;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            thought.SetBubbleText("that water is electric, i shouldnt touch it");
            thought.ShowBubbleForSeconds(2);
        }

        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
