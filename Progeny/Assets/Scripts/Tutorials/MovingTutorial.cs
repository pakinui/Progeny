using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTutorial : MonoBehaviour
{
    public GameObject checkpoint;

    private Player player;
    private Rigidbody2D rb;
    private Rigidbody2D checkpointRb;

    private GameObject leftAndRight;
    private GameObject interact;
    

    private bool checkPointReached = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        checkpointRb = checkpoint.GetComponent<Rigidbody2D>();

        leftAndRight = GameObject.Find("Move A/D");
        interact = GameObject.Find("interact E");
        interact.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        /**
            if player hasnt moved to checkpoint 
            move the square to follow the player
        */
        
        rb.transform.position = new Vector3 (player.transform.position.x + 3.0f , player.transform.position.y, player.transform.position.z);

        if(player.transform.position.x > checkpointRb.transform.position.x) checkPointReached = true;

        if(checkPointReached){
            interact.SetActive(true);
            leftAndRight.SetActive(false);
            
            if(Input.GetKeyDown("e")){
                Destroy(this.gameObject);
            }
            
            

        }
    }
}
