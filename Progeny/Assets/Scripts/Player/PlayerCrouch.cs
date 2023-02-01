using System;
using UnityEngine;
using Utility;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchDuration = 0.2f;
    public GameObject ceilingCheck;
    private TriggerCheck2D mCeilingTriggerCheck2D;
    private bool mCrouchPressed;
    
    private Animator mAnimator;
    private Player mPlayer;
    private BoxCollider2D collider;
    
    
    private Color mOriginalColor;
    private Color mCrouchColor;
    public Vector2 mTargetColliderSize = new Vector2(0.75f, 1f);
    public Vector2 mTargetColliderOffset = new Vector2(0, -0.5f);
    private Vector2 mOriginalColliderSize;
    private Vector2 mOriginalColliderOffset;
    
    private float mCrouchTimer;
    private static readonly int Crouching = Animator.StringToHash("Crouching");

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        // Start is called before the first frame update
        mPlayer = GetComponent<Player>();
        mCeilingTriggerCheck2D = ceilingCheck.GetComponent<TriggerCheck2D>();
        
        collider = GetComponent<BoxCollider2D>();
        mOriginalColliderOffset = collider.offset;
        mOriginalColliderSize = collider.size;
        mOriginalColor = GetComponent<SpriteRenderer>().color;
        mCrouchColor = new Color(1, 0, 1, 1f);
    }
    
    void BeginCrouch()
    {
        // set player to crouching
        mPlayer.setCrouching(true);
    }
    
    void EndCrouch()
    {
        // set player to not crouching
        mPlayer.setCrouching(false);
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            mCrouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            mCrouchPressed = false;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(30,30,100,100), "Crouching: " + mCrouchPressed);
    }

    private void FixedUpdate()
    {
        
        if (mPlayer.isClimbing() || mPlayer.isPushing()) return;
        
        // Crouch
        if (!mCrouchPressed && !mCeilingTriggerCheck2D.Triggered())
        {
            // stand up
            mCrouchTimer -= Time.fixedDeltaTime;
            if (mCrouchTimer <= 0)
            {
                mCrouchTimer = 0;
                EndCrouch();
            }
        } 
        else if (mCrouchPressed)
        {
            // crouch
            BeginCrouch();
            mCrouchTimer += Time.fixedDeltaTime;
            if (mCrouchTimer >= crouchDuration)
            {
                mCrouchTimer = crouchDuration;
            }
        }
        
        // Update animator
        if (mAnimator != null)
        {
            mAnimator.SetFloat(Crouching, mCrouchTimer / crouchDuration);
        }
        else 
        {
            if (mCrouchTimer > 0)
                Debug.LogWarning("Animator is null");
            // Manually update collider to foot position
            collider.size = Vector2.Lerp(mOriginalColliderSize, mTargetColliderSize, mCrouchTimer / crouchDuration);
            collider.offset = Vector2.Lerp(mOriginalColliderOffset, mTargetColliderOffset, mCrouchTimer / crouchDuration);
            // Manually update sprite color
            GetComponent<SpriteRenderer>().color = Color.Lerp(mOriginalColor, mCrouchColor, mCrouchTimer / crouchDuration);
        }
    }
}