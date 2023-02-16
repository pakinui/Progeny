using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAttack : MonoBehaviour
{
    // reference to the player
    Player player;
    // damage dealt to player on successful atttack (currently does a third)
    public float attackDamage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update(){}

    // attack
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !this.transform.parent.gameObject.GetComponent<GroundEnemy>().dead)
        {    
            float newHealth;
            player.SetCurrentHealth(newHealth = player.GetCurrentHealth() - attackDamage);
            //if (newHealth <= 0) {player.Die();}
            //if(newHealth <= 0){player.SetCu}
        
            player.setRed(true);
        }
    }
}
