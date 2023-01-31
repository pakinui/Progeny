using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    // reference to the 'Player' script
    private Player player;
    // reference to the RigidBody
    private Rigidbody2D rb;
    // reference to the ledge layer mask
    public LayerMask ledge;

    // which boxes are overlapping with a ledge layer
    private bool topBox, midBox;
    public float xOffset, xSize, ySize, topYOffset, midYOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
