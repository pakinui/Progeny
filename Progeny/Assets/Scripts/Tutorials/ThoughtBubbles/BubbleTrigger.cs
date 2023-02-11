using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrigger : MonoBehaviour
{
    //if script is on a trigger then it uses this text
    public string bubbleText;
    public bool isTimed = false; //should the bubble be on a timer
    public float bubbleTime; //sec for timed bubble

    //private Player player;
    private ThoughtBubble bubble;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
        if script is on a trigger then use the provided string 'bubbleText'
        and then show the bubble either with or without a timer
        !isTimed - 'showBubble()'
        isTimed = 'showBubbleForSeconds(timeRemaining)'
    */
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            bubble.SetBubbleText(bubbleText);

            if(!isTimed){
                bubble.ShowBubble();
            }else{
                bubble.ShowBubbleForSeconds(bubbleTime);
                
            }
            Destroy(this.gameObject);

        }
    }
}
