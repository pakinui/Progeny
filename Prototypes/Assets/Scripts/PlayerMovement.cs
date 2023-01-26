using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    //public so cutscenes prototype can access it
    public Rigidbody2D rb;
    private float direction;
    private bool facingRight = true;
    public AudioClip footsteps;
    AudioSource audioSource;

    //to stop player from being able to move during cutscenes.
    public static bool moving = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
       //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            direction = Input.GetAxis("Horizontal");

            if (direction > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction < 0 && facingRight)
            {
                Flip();
            }
            Vector2 velocity = new Vector2(direction * speed, rb.velocity.y);
            rb.velocity = velocity;
            // if (direction != 0 && !audioSource.isPlaying)
            // {
            //     audioSource.PlayOneShot(footsteps, 1f);
            // }

            // TODO : jump
        }
        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }


    public void setAbilityToMove(bool b){
        moving = b;
    }
}
