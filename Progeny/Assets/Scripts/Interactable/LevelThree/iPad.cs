using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class iPad : MonoBehaviour
{
    
    public GameObject background;
    public GameObject iPadVideo;
    public GameObject display; // E to display
    public Canvas canvas;
    public GameObject speech;
    public TextMeshProUGUI text;
    public Animator anim;
    public Sprite[] scientistImg;
    public Sprite[] scientistImgCharged;
    
    public GameObject scientistObject;
    public BarnFire barn;// to access booleans

    private Player player;
    private ThoughtBubble thought;
    private SpriteRenderer scientist;
    //story stuff
    //text to display
    public TextAsset file;
    public TextAsset secondFile;
    private string[] textLines;
    private string[] secondLines;

    //what line currently on
    private int currLine;

    //what line to stop reading
    private int endLine;
    

    private bool watched = false;
    private bool contact = false;
    private bool watching = false;
    private bool storyTextDone = false;
    private bool pause = true;
    private bool charged = false;

    private float flatTime = 1.2f;
    private int currImg = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
       thought =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
       //anim = GetComponent<Animator>();
       textLines = null;
        scientist = scientistObject.GetComponent<SpriteRenderer>();
        currLine = 0;
        endLine = 0;
        speech.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(contact && !watching && !watched){
            if(Input.GetKeyDown("e")){
                thought.hideBubble();
                player.stopPlayerMovement();
                background.SetActive(true);
                watching = true;
                display.SetActive(false);
                iPadVideo.SetActive(true);
    
            }
        }else if(watching && !watched){
            //currently watchign video
            if(pause){
                if(Input.GetMouseButtonDown(0)){
                    //clicked pause button
                    anim.SetTrigger("Start");
                    pause = false;
                    speech.SetActive(true);//speech img
                    if(file != null || secondFile != null){
                        textLines = (file.text.Split('\n'));
                        secondLines = (secondFile.text.Split('\n'));
                    }else{
                        Debug.Log("file is null");
                    }

                    if(endLine == 0 && !charged){
                        endLine = textLines.Length-1;
                    }else if(endLine == 0 && charged){
                        endLine = secondLines.Length-1;
                    }
                    NextSlide();
                }
            }else{
                if(Input.GetKeyDown(KeyCode.Space)){
                    
                    currLine++;
                    NextSlide();
                }
            }
           
        }else if(watched && watching){
            if(flatTime <= 0){
                CloseVideo();
            }else{
                flatTime -= Time.deltaTime;
            }
        }
    }
//221.3287 -2.990382
    
    public void Charged(){
        watched = false;
        watching = true;
        charged = true;
        iPadVideo.SetActive(true);
        speech.SetActive(true);
        anim.SetTrigger("SecondPause");
        background.SetActive(true);
        textLines = null;
        currLine = 0;
        endLine = 0;
        scientistObject.SetActive(true);
        currImg = 0;
        pause = true;
        contact = false;
        gameObject.SetActive(true);

    }

    public void CloseVideo(){
        
        background.SetActive(false);
        watching = false;
        watched = true;
        iPadVideo.SetActive(false);
        speech.SetActive(false);
        player.startPlayerMovement();
        gameObject.SetActive(false);
        if(!charged){
            thought.SetBubbleText("you've got to be joking me it went flat?!?!");
            thought.ShowBubbleForSeconds(2);
        }else{
            Destroy(this.gameObject);
        }
        
    }


    //to change wife sprite on screen
    public void NextSlide(){

        if(currLine > endLine ){
            //video done
            speech.SetActive(false);
            scientistObject.SetActive(false);
            if(!charged){
                anim.SetTrigger("flat");
            
            }else{
                //will make the player pick ending
                barn.finishedVideo = true;
                CloseVideo();

            }
            
            watched = true;
        }else{
            if(!charged){
                text.text = textLines[currLine];
                scientist.sprite = scientistImg[currImg++];
            }else{
                text.text = secondLines[currLine];
                scientist.sprite = scientistImgCharged[currImg++];
            }
            
            
        }
    }


     // when player enters box collider and is close 
    // enough to interact
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = true;
            display.SetActive(true);
            
        }
    }

    // when player exits box collider and is no longer
    // close enough to interact
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            contact = false;
            display.SetActive(false);
        }
    }
}
