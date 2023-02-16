using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{
    
    public Animator anim;
    private GroundEnemy ground;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ground = GetComponent<GroundEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
