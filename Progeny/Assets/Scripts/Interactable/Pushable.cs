using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    class to add to a pushable object (the PushObject prefab)
    which can change whether the obj is pushable or not.
    this updates the climb triggers which tell the pushing 
    animation to run or not.
*/
public class Pushable : MonoBehaviour
{

    //is obj able to be pushed
    public bool isPushable = true;

    private Rigidbody2D rb;

    //triggers to tell is animation should be playing
    public MovingClimb leftClimbTrigger;
    public MovingClimb rightClimbTrigger;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetPushable(isPushable); 
    }

    

    public void SetPushable(bool b){
        isPushable = b;
        if(isPushable){
            rb.gravityScale = 5.0f;
            leftClimbTrigger.pushable = true;
            rightClimbTrigger.pushable = true;
        }else{
            rb.gravityScale = 100.0f;
            leftClimbTrigger.pushable = false;
            rightClimbTrigger.pushable = false;
        }
    }
}
