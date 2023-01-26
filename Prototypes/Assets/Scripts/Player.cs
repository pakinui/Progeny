using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameMaster gm;
    //private Vector3 lastCheckpointPos;

    public float playerHealth = 100;
    private GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0){
            Death();
        }
    }

    void Death(){
        // death screen GUI with respawn button goes here
        // code below is for after respawn button is hit
        Debug.Log("DEAD");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            other.transform.parent.GetComponent<Enemy>().Attack(this.gameObject);
        }else if(other.tag == "Respawn"){
            gm.lastCheckpointPos = transform.position;
            Debug.Log("checkpoint reached: " + transform.position);
        }
    }
}
