using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When enemy collides with an object with a
    // collider that is a trigger...
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            Destroy(other.gameObject);
            enemyHealth -= 1;
            if(enemyHealth == 0){
                Destroy(this.gameObject);
            }
        }
        
    }
}
