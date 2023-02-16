using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasJerryCan : MonoBehaviour
{
    public GameObject block;
    public PetrolInteract cart;

    private Player player;
    private ThoughtBubble thought;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.Find("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cart.hasCart){
               Destroy(this.gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            thought.SetBubbleText("i should get those jerry cans to kill the monster.");
            thought.ShowBubbleForSeconds(2);
        }
    }
}
