using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceEnemyAttack : MonoBehaviour
{
    // reference to the player
    Player player;
    // damage dealt to player on successful atttack
    public float attackDamage = 100f/3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update(){}

    // attack
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.health -= attackDamage;
            Debug.Log("OUCH! health: " + player.health);
            if (player.health <= 0) {player.Die();}
        }
    }
}
