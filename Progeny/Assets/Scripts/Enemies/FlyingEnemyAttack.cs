using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    // reference to the player
    Player player;
    // reference to the projectile's rigidbody
    Rigidbody2D rb;
    // damage dealt to player on successful atttack (currently does a third)
    public float attackDamage = 10f;
    // force behind the projectile's velocity
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        // target players location when projectile instantiated
        Vector3 target = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y+3);
        // find direction of shot
        Vector3 direction = target - transform.position;
        // set velocity
        rb.velocity = new Vector3(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update(){
        if (!VisibleCheck.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    // attack
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {    
            float newHealth;
            player.SetCurrentHealth(newHealth = player.GetCurrentHealth() - attackDamage);
            Debug.Log("OUCH! health: " + newHealth);
            if (newHealth <= 0) {player.Die();}

            player.setRed(true);
        }
    }
}
