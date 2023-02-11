using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    // reference to the Player script
    private Player player;
    // reference to the melee weapon
    public GameObject meleeWeapon;

    public bool hasObtainedMelee = true;
    private bool meleeEnabled = false;
    // melee sound
    public AudioClip meleeSound;
    // speed of attack
    public float attackDuration;
    private float attackLeft;
    // cooldown variables
    public float cooldown;
    private float cooldownLeft = 0f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        audioSource = player.GetComponent<AudioSource>();
        if(hasObtainedMelee){
            meleeEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isAiming() && !player.isCrouching() && meleeEnabled && Input.GetMouseButtonDown(0) && cooldownLeft <= 0){
            Swing();
        }
        if(player.isHitting()){
            attackLeft -= Time.deltaTime;
            if(attackLeft <= 0f){
                // hide weapon and change player state
                meleeWeapon.SetActive(false);
                player.setHitting(false);
            }
        } else if(!player.isHitting() && cooldownLeft > 0){
            cooldownLeft -= Time.deltaTime;
        }
    }

    public float GetAttackLeft(){
        return attackLeft;
    }

    public float GetCooldownLeft(){
        return cooldownLeft;
    }

    public void setMelee(bool meleeEnable){
        if (hasObtainedMelee && meleeEnable){
            meleeEnabled = meleeEnable;
        }
        else{
            meleeEnabled = false;
        }
    }
    private void Swing(){
        // change player state and show weapon
        player.setHitting(true);
        meleeWeapon.SetActive(true);
        audioSource.PlayOneShot(meleeSound, 0.5f);
        //Debug.Log("swing and a miss");
        // BELOW LINE NEEDS FIXING - CURRENTLY DOESN'T ROTATE BACK
        //meleeWeapon.transform.RotateAround(transform.position, Vector3.forward, 360*Time.deltaTime);
        attackLeft = attackDuration;
        cooldownLeft = cooldown;
    }
}
