using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    private bool greenBox, redBox;
    public float redXOffset, redYOffset, redXSize, redYSize, greenXOffset, greenYOffset, greenXSize, greenYSize;
    
    private Rigidbody2D rb;
    private float startingGrav;
    public LayerMask groundMask;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
        facingRight = GetComponent<PlayerMovement>().facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if(facingRight != GetComponent<PlayerMovement>().facingRight){
            Flip();
        }

        greenBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize), 0f, groundMask);
        redBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize), 0f, groundMask);

        if(greenBox && !redBox && !PlayerVariables.isClimbing && Input.GetKey("space")){
            PlayerVariables.isClimbing = true;
            if(facingRight){
                transform.position = new Vector2(transform.position.x + .5f, transform.position.y + 2f);
            }else{
                transform.position = new Vector2(transform.position.x - .5f, transform.position.y + 2f);
            }
            PlayerVariables.isClimbing = false;
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize));
    }

    private void Flip(){
        facingRight = !facingRight;
        redXOffset *= -1;
        greenXOffset *= -1;
    }
}
