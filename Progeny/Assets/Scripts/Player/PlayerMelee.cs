using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    // reference to the Player script
    private Player player;
    // reference to the melee weapon
    public GameObject meleeWeapon;
    // speed of attack
    public float attackDuration;
    private float attackLeft;
    // cooldown variables
    public float cooldown;
    private float cooldownLeft = 0f;
    // direction of swing
    private int swingDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        if(!player.isFacingRight()) {swingDirection = -1;}
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isAiming() && !player.isCrouching() && Input.GetMouseButtonDown(0) && cooldownLeft <= 0){
            Swing();
        }
        if(player.isHitting()){
            attackLeft -= Time.deltaTime;
            if(attackLeft <= 0f){
                // hide weapon and change player state
                meleeWeapon.SetActive(false);
                player.setHitting(false);
                attackLeft = attackDuration;
            }
        } else if(!player.isHitting() && cooldownLeft > 0){
            cooldownLeft -= Time.deltaTime;
        }
    }

    private void Swing(){
        // change player state and show weapon
        player.setHitting(true);
        meleeWeapon.SetActive(true);
        //Debug.Log("swing and a miss");
        meleeWeapon.transform.RotateAround(transform.position, Vector3.forward, 360*Time.deltaTime);
        attackLeft = attackDuration;
        cooldownLeft = cooldown;
    }
}
