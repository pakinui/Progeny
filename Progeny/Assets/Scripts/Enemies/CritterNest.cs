using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterNest : MonoBehaviour
{
    //track colour
    private bool isRed = false;
    public float redDuration = 1;
    private float redTimer;
    private Transform player; // reference to the player (for distance)
    public GameObject critter; // reference to the critter object to be spawned
    public int health; // shots taken to destroy nest
    public float spawnFrequency; // rate at which critters are spawned
    private float spawnTimer; // keeps track of frequency between spawns
    // public int spawnQuantity = 1; // quantity of critters to be spawned each spawn
    // public int spawnLimit; // limit to how many critters can be spawned
    public bool rangeEnabled;
    public float spawnRange; // range in which the player has to be in order to spawn a critter
    //sprite renderer for colour
    private SpriteRenderer sr;
    private int startingHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        spawnTimer = spawnFrequency;
        sr = GetComponent<SpriteRenderer>();
        startingHealth = health;
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

        if (isRed){
            redTimer -= Time.deltaTime;
            if (redTimer <= 0){
                sr.color = new Color(255f, 255f, 255f, 1f);
                isRed = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            Destroy(other.gameObject);
            health--;
            if(health == 0){
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
            sr.color = new Color(255f, 0f, 0f, 1f);
            isRed = true;
            redTimer = redDuration;
        }
    }


    public void ResetNest(){
        this.gameObject.SetActive(true);
        health = startingHealth;
        redTimer = 0;
        spawnTimer = spawnFrequency;
    }

    public void DestroyNest(){
        Destroy(this.gameObject);
    }
}
