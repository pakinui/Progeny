using System;
using UnityEngine;
using Utility;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchDuration = 0.2f;
    public GameObject ceilingCheck;
    private TriggerCheck2D ceilingTriggerCheck2D;
    private bool crouchPressed;
    
    private Player player;
    private BoxCollider2D collider;
    
    public Vector2 targetColliderSize = new Vector2(0.45f, 1.25f);
    private Vector2 originalColliderSize;
    
    private float crouchTimer;

    void Start()
    {
        // Start is called before the first frame update
        player = GetComponent<Player>();
        ceilingTriggerCheck2D = ceilingCheck.GetComponent<TriggerCheck2D>();
        
        collider = GetComponent<BoxCollider2D>();
        originalColliderSize = collider.size;
    }
    
    void BeginCrouch()
    {
        // set player state
        player.setCrouching(true);
        // decrease the player collider's size and adjust its position to avoid a short fall
        collider.size = new Vector2(targetColliderSize.x, targetColliderSize.y);
        //collider.size = Vector2.Lerp(originalColliderSize, targetColliderSize, crouchDuration);
        transform.position = new Vector2(transform.position.x, transform.position.y - ((originalColliderSize.y - targetColliderSize.y) / 2));
    }
    
    void EndCrouch()
    {
        // set player state
        player.setCrouching(false);
        // increase the player collider's size and adjust its position
        collider.size = new Vector2(originalColliderSize.x, originalColliderSize.y);
        transform.position = new Vector2(transform.position.x, transform.position.y + ((originalColliderSize.y - targetColliderSize.y) / 2));
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }
    }

    private void FixedUpdate()
    {
        
        if (player.isClimbing() || player.isPushing()) return;
        
        // Crouch
        if (!crouchPressed && !ceilingTriggerCheck2D.Triggered() && player.isCrouching())
        {
            // stand up
            crouchTimer -= Time.fixedDeltaTime;
            if (crouchTimer <= 0)
            {
                crouchTimer = 0;
                EndCrouch();
            }
        } 
        else if (crouchPressed && !player.isCrouching())
        {
            // crouch
            BeginCrouch();
            crouchTimer += Time.fixedDeltaTime;
            if (crouchTimer >= crouchDuration)
            {
                crouchTimer = crouchDuration;
            }
        }
        
 
        if (crouchTimer > 0){}
        // Manually update collider to foot position
        //collider.size = Vector2.Lerp(mOriginalColliderSize, mTargetColliderSize, crouchTimer / crouchDuration);
        //collider.offset = Vector2.Lerp(mOriginalColliderOffset, mTargetColliderOffset, crouchTimer / crouchDuration);

    }
}