using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnFire : MonoBehaviour
{

    
    public GameObject display;
    public GameObject fireAnimation;
    public GameObject cart;

    //ipad stuff
    public GameObject background;
    public Animator anim;
    public GameObject iPadVideo;
    public GameObject speech;

    public iPad tablet;

    

    public bool finishedVideo = false;

    
    private Player player;
    private ThoughtBubble thought;
    private FadeToBlack ftb;
    private GameMaster gm;
    private bool contact = false;
    private bool transition = false;
    private float timer = 0;
    private bool waiting = false;
    private bool videoPlaying = false;//second video
    

    private float timeRemaining = 2f;
    private float talkTime = 3f;
    private bool talking = false;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.Find("ThoughtBubble").GetComponent<ThoughtBubble>();
        ftb = GameObject.Find("Canvas").GetComponent<FadeToBlack>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    //Update is called once per frame
    void Update()
    {

        if(finishedVideo){
            //ipad video done
            //decision to light barn on fire or not

        }else if(contact){
            if(Input.GetKeyDown("e") && !talking){
                display.SetActive(false);
                
                    player.stopPlayerMovement();
                thought.SetBubbleText("i just need to cover this place in the petrol");
                    thought.ShowBubbleForSeconds(2);
                   
                
               
                

            }else if(talking){
                    cart.SetActive(false);
                    //add black screen for a lil transistion
                    StartCoroutine(ftb.FadeBlackSquare(true, 0.5f));
                    contact = false;
                    transition = true;
                
            }

             if(!talking && talkTime <= 0){
                    
                    talking = true;
                }else if(!talking){
                    talkTime -= Time.deltaTime;
                }
        }
        if(transition){

            if (ftb.isBlack){

                StartCoroutine(ftb.FadeBlackSquare(false, 0.5f));
                player.transform.position = new Vector3(221.3287f, -2.990383f, 7.8f);
                //fireAnimation.SetActive(true);
                thought.SetBubbleText("the iPad is charged, ill finish the video while I wait");
                thought.ShowBubbleForSeconds(2);
                player.Flip();
                transition = false;
                timer = 4f;
            }
        }
        if(timer > 0){

            timer -= Time.deltaTime;
            if (timer <= 0){
  
                tablet.Charged();
                videoPlaying = true;
            }
        }
    }


    

    public void EndCutscene(){
        Debug.Log("game over");
    }


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = true;
            display.SetActive(true);
            //player.stopPlayerMovement();
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            contact = false;
            display.SetActive(false);
        }
    }
}// 209.3148 -2.990383  7.8
