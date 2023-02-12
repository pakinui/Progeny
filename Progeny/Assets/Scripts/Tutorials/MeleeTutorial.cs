using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTutorial : MonoBehaviour
{
    
    public TextAsset meleeTextFile;
    public GroundEnemy enemy;
    public GameObject dashTrigger;
    public GameObject displayPrefab;
    public MeleeBlock block;

    private GameObject display;
    
    private Player player;
    private StoryText story;
    private DashTutorial dashTut;
    private GroundEnemy dashEnemy;
    private bool interactZone = false;
    private bool meleePickedUp = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        story = GameObject.FindWithTag("StorySquare").GetComponent<StoryText>();
        
        dashTut = dashTrigger.GetComponent<DashTutorial>();
    }



    // Update is called once per frame
    void Update()
    {
        if(interactZone && Input.GetKeyDown("e")){
            //gameObject.SetActive(false);
            block.melee = true;
            Destroy(display);
            meleePickedUp = true;
            PlayerMelee pm = player.GetComponent<PlayerMelee>();
            pm.hasObtainedMelee = true;
            player.stopPlayerMovement();
            story.PlayStoryText(meleeTextFile);
        }
        
        if(story.storyComplete && meleePickedUp){

            dashTut.StartDashTutorial();
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            interactZone = true;
            display = Instantiate(displayPrefab, this.transform.parent);
            display.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            display.transform.Translate(0, 2.5f, 0);
        }
        
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            interactZone = false;
            Destroy(display);
        }
        
    }
}
