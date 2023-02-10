using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolInteract : MonoBehaviour
{
    
    public GameObject beforePetrol; // jerry cans and cart empty
    public GameObject afterPetrol;// no jerry cans and cart ready to push
    public GameObject display; // E to display


    private Player player;
    private bool contact =  false;
    private ThoughtBubble thought;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.Find("ThoughtBubble").GetComponent<ThoughtBubble>();
        
        beforePetrol.SetActive(true);
        afterPetrol.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = true;
            display.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = false;
            display.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(contact){
            

            //thought bubble to hint the player why the
            //need to cans
            thought.SetBubbleText("I could use those jerry cans to start a fire and kill the monsterI could use those jerry cans to start a fire and d");
            thought.ShowBubbleForSeconds(2);

            if(Input.GetKeyDown("e")){
                display.SetActive(false);
                
                // lil transition TODO

                // and then cut to after petrol station
                // and cart is full of jerry cans and pushable
                beforePetrol.SetActive(false);
                afterPetrol.SetActive(true);
            }
        }
    }
}
