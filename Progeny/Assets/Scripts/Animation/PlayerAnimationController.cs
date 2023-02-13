using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    // Reference to animator component
   public Animator anim;
   private Player player;
    
    private bool startedHit = false;

    

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the reference to the Animator component
      anim = GetComponent<Animator>();
      player = GameObject.Find("Player").GetComponent<Player>();
    
    }

    // Update is called once per frame
    void Update()
    {
       FalseAll();
        
        
        if(player.GetCurrentHealth() <= 0)anim.SetBool("dying", true);

        else if(player.isHitting()){ 
            if(!startedHit){
                anim.SetBool("isStriking", true);
                anim.SetTrigger("strike"); 
            }else{
                startedHit = true;
            }
            
        }else if(!player.isHitting()){
            startedHit = false;
        }
       if(player.isFalling()) anim.SetBool("falling", true);
        else if(player.isClimbing()){
            
            anim.SetBool("climb", true);
            anim.SetTrigger("climbing");    
        } 
        else if(player.isPushing()) anim.SetBool("pushing", true);

        if(player.isAiming())anim.SetBool("aiming", true);
        
        if(player.isMoving()) anim.SetBool("moving", true);
        else anim.SetBool("idle", true);

        if(player.isCrouching()) anim.SetBool("crouch", true);

        if(player.isMoving() && !player.isCrouching()) anim.SetBool("walk", true);


        

    }





    private void FalseAll(){
        anim.SetBool("moving", false);
        anim.SetBool("idle", false);
        anim.SetBool("crouch", false);
        anim.SetBool("walk", false);
        anim.SetBool("pushing", false);
        anim.SetBool("climb", false);
        anim.SetBool("falling", false);
        //anim.SetBool("shortClimbing", false);
        // anim.SetBool("shootingWalk", false);
        // anim.SetBool("shootingCrouch", false);
        anim.SetBool("aiming", false);
        anim.SetBool("isStriking", false);
        anim.SetBool("dying", false);
    }

}

