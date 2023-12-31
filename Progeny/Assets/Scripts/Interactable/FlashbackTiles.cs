using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FlashbackTiles : MonoBehaviour
{

    public GameObject green;
    public GameObject orange;

    public string afterThought;
   

    private float timeRemaining;
    private bool countdown = false;
    private float left;
    private float right;
    private Rigidbody2D rb; //tiles rb to get position
    private bool completed = false;
    private ThoughtBubble thought;


    // Start is called before the first frame update
    void Start()
    {
        
        thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
        orange.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        left = (rb.position.x - 10f);
        right = (rb.position.x + 10f);
        timeRemaining = 2f;
    }


    void Update(){
        if(countdown && !completed){
            if(timeRemaining <= 0){
                countdown = false;
                EndFlashback();
            }else{
                timeRemaining -= Time.deltaTime;

                if(rb.position.x > left && rb.position.x < right){
                    

                }else{
                    EndFlashback();
                }
            } 
 
        }
    }

    private void EndFlashback(){
        orange.SetActive(false);
        green.SetActive(true);
        completed = true;
        Destroy(orange);
        if(afterThought != ""){
            //thought bubble after the flashback
            thought.SetBubbleText(afterThought);
            thought.ShowBubbleForSeconds(2);
        }
        Destroy(this.gameObject);//so errors arent thrown after dying and flashback not being found
    }


    void OnTriggerEnter2D(Collider2D tile){
        if(tile.tag == "Player"){
            thought.hideBubble();
            orange.SetActive(true);
            green.SetActive(false);
            countdown = true;
        }
    }

   

   
}
