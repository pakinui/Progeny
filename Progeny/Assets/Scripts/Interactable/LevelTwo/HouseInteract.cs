using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteract : MonoBehaviour
{
    public GameObject bigPhoto; 
    public GameObject display; // E to display
    public GameObject interactBackground;
    public string thoughtText;

    //which obj is this one
    public bool familyPhoto = false;
    public bool workLetter = false;
    public bool calendar = false;
    

    //to change booleans in doorblock
    public DoorBlock block;

    private Player player;
    private ThoughtBubble bubble;
    private bool contact = false;
    private bool imgOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player presses e and img is not open yet
        if(contact && !imgOpen){
            if(Input.GetKeyDown("e")){
                player.stopPlayerMovement();
                bigPhoto.SetActive(true);
                interactBackground.SetActive(true);
                imgOpen = true;
                display.SetActive(false);

                if(familyPhoto) block.photo = true;
                else if(workLetter) block.letter = true;
                else if(calendar) block.calendar = true;
                
            }
        }else if(contact && imgOpen){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartThought();
            }
        }
    }

    private void StartThought(){
        bigPhoto.SetActive(false);
        interactBackground.SetActive(false);
        imgOpen = false;
        player.startPlayerMovement();
        bubble.SetBubbleText(thoughtText);
        bubble.ShowBubbleForSeconds(3);
        Destroy(this.gameObject);// so player cant see them a second time
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

