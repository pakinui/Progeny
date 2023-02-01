using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceEnemyAttack : MonoBehaviour
{
    // reference to the player
    Player player;
    // damage dealt to player on successful atttack (currently does a third)
    public float attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        attackDamage = player.maxHealth / 3f;
    }

    // Update is called once per frame
    void Update(){}

    // attack
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {    
            float newHealth;
            player.SetCurrentHealth(newHealth = player.GetCurrentHealth() - attackDamage);
            Debug.Log("OUCH! health: " + newHealth);
            if (newHealth <= 0) {player.Die();}
        }
    }
}
