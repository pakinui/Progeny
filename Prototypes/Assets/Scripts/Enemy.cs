using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 3;
    public float attackDamage = 34;
    public float attackCooldown = 3;
    private float cooldownStatus = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownStatus > 0){
            cooldownStatus -= Time.deltaTime;
        }
    }

    // When enemy collides with an object with a
    // collider that is a trigger...
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            Destroy(other.gameObject);
            enemyHealth -= 1; // TODO: Magic number ?
            if(enemyHealth == 0){
                Destroy(this.gameObject);
            }
        }
    }

    public void Attack(GameObject player){
        if(cooldownStatus <= 0){
            // code for attack animation goes here
            player.GetComponent<Player>().playerHealth -= attackDamage;
            Debug.Log("attacked player for " + attackDamage + ". player health = " + player.GetComponent<Player>().playerHealth);
            cooldownStatus = attackCooldown;
        }else{
            Debug.Log("attack cooldown: " + cooldownStatus);
        }
    }
}
