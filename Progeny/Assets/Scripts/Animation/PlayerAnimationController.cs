using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    // Reference to animator component
   Animator anim;
   private Player player;
    


    

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
        // if(player.isClimbing()){
            
        //     anim.SetBool("climb", true);


        // }else if(player.isFalling()){
        //     ///FalseAll();
        //     anim.SetBool("falling", true);
        // }else if(player.isHitting()){
        //     //FalseAll();
            
        //     anim.SetTrigger("strike");
            
        // }else if(player.isAiming() && !player.isMoving()){
        //     //idle shooting
        //     anim.SetBool("aiming", true);
        //     if(player.isCrouching()){
                
        //         anim.SetBool("shootingCrouch", true);
        //     }else{
                
        //         anim.SetBool("idleStand", true);
        //         anim.SetBool("aiming", true);
        //     }
         
            
        // }else if(player.isMoving()){
        //     //stopAim();//player isnt aiming
        //     if(player.isPushing()){
        //         //FalseAll();
        //         anim.SetBool("pushing", true);
        //     }else if(player.isCrouching()){
        //         //FalseAll();
        //         if(player.isAiming()){
        //             anim.SetBool("shootingCrouch", true);
        //         }else{
        //             anim.SetBool("crouching", true);
        //         }
                    
                
                
        //     }else{
        //         //FalseAll();
        //         if(player.isAiming()){
        //             anim.SetBool("shootingWalk", true);
        //         }else{
        //             //stopAim();//player isnt aiming
        //             anim.SetBool("walking", true);
        //         }
                
        //     }

        // }else{
        //     if(player.isCrouching()){
        //         //idle crouch
        //         //FalseAll();
        //         anim.SetBool("idleCrouch", true);
        //     }else{
        //         //standing idle
        //         //FalseAll();
        //         anim.SetBool("idleStand", true);
        //     }
        // }
        if(player.GetCurrentHealth() <= 0)anim.SetBool("dying", true);

        else if(player.isHitting()){ 
            anim.SetBool("isStriking", true);
            anim.SetTrigger("strike"); 
        } 
        else if(player.isFalling()) anim.SetBool("falling", true);
        else if(player.isClimbing()) anim.SetBool("climbing", true);
        else if(player.isPushing()) anim.SetBool("pushing", true);
        else if(player.isAiming())anim.SetBool("aiming", true);
        
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

