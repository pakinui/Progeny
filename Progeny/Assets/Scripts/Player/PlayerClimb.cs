using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    // reference to the 'Player' script
    private Player player;
    // reference to the ledge layer mask
    public LayerMask ledgeMask;

    // box offsets
    public float xOffset;
    public float highYOffset = 1f;
    public float midYOffset = 0.4f;
    public float lowYOffset = -0.3f;
    // box positions
    private Vector2 highPos;
    private Vector2 midPos;
    private Vector2 lowPos;
    // box size
    private Vector2 boxSize = new Vector2(0.3f, 0.1f);

    // which boxes are overlapping with ledge layer
    private bool highOver, midOver, lowOver;
    
    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // update box positions
        highPos = new Vector2(transform.position.x + xOffset, transform.position.y + highYOffset);
        midPos = new Vector2(transform.position.x + xOffset, transform.position.y + midYOffset);
        lowPos = new Vector2(transform.position.x + xOffset, transform.position.y + lowYOffset);

        // check for box overlaps with ledge layer
        highOver = Physics2D.OverlapBox(highPos, boxSize, 0f, ledgeMask);
        midOver = Physics2D.OverlapBox(midPos, boxSize, 0f, ledgeMask);
        lowOver = Physics2D.OverlapBox(lowPos, boxSize, 0f, ledgeMask);

        // climb
        if(midOver && !highOver && !player.isClimbing() && Input.GetKey("space")){
            player.setClimbing(true);
            if(player.isFacingRight()) {
                transform.position = new Vector2(transform.position.x + .5f, transform.position.y + 2f);
            } else {
                transform.position = new Vector2(transform.position.x - .5f, transform.position.y + 2f);
            }
            player.setClimbing(false);
        }

        // mantle
        if(lowOver && !midOver && !player.isClimbing() && !player.isVaulting() && Input.GetKey("space")){
            player.setVaulting(true);
            if(player.isFacingRight()) {
                transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
            } else {
                transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
            }
            player.setVaulting(false);
        }
    }

    // draw gizmos when player is selected
    // helps to visualise the boxes
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(highPos, boxSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(midPos, boxSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(lowPos, boxSize);
    }
}
