using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterNest : MonoBehaviour
{
    public Transform player; // reference to the player (for distance)
    public GameObject critter; // reference to the critter object to be spawned
    public int health; // shots taken to destroy nest
    public float spawnFrequency; // rate at which critters are spawned
    private float spawnTimer; // keeps track of frequency between spawns
    // public int spawnQuantity = 1; // quantity of critters to be spawned each spawn
    // public int spawnLimit; // limit to how many critters can be spawned
    public bool rangeEnabled;
    public float spawnRange; // range in which the player has to be in order to spawn a critter
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        spawnTimer = spawnFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(Mathf.Abs(player.position.x - transform.position.x) <= spawnRange || !rangeEnabled)
        {
            if(spawnTimer <= 0)
            {
                //for(int i = 0; i < spawnQuantity; i++){
                Transform spawn = Instantiate(critter).transform;
                spawn.position = transform.position;
                spawnTimer = spawnFrequency;
                //}
                // spawnLimit--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            Destroy(other.gameObject);
            health--;
            if(health == 0){
                Destroy(this.gameObject);
            }
        }
    }
}
