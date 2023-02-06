using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReminder : MonoBehaviour
{
    
    private Player player;
    private ThoughtBubble thought;

    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player" && player.gun == null){
            thought.SetBubbleText("maybe i should take that gun. . .");
            thought.ShowBubble();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gun != null){
            thought.hideBubble();
            Destroy(this.gameObject);
        }
    }
}
