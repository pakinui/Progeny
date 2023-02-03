using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTutorial : MonoBehaviour
{
    
    public TextAsset meleeTextFile;
    
    private Player player;
    private StoryText story;
    private bool interactZone = false;
    private bool meleePickedUp = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        
    }



    // Update is called once per frame
    void Update()
    {
        if(interactZone && Input.GetKeyDown("e")){
            //gameObject.SetActive(false);

            meleePickedUp = true;
            gameObject.SetActive(false);
            player.stopPlayerMovement();
            story.PlayStoryText(meleeTextFile);
        }else if(story.storyComplete && meleePickedUp){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            interactZone = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            interactZone = false;
        }
        
    }
}
