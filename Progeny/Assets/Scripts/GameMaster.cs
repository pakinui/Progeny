using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour 
{
    private float slowMoDuration;
    public float timeOrder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (slowMoDuration > 0f) {
            if(Time.timeScale == 1f){
                Time.timeScale = timeOrder;
            }
            slowMoDuration -= Time.fixedDeltaTime;
        } else if (slowMoDuration <= 0f && Time.timeScale == timeOrder) {
            Time.timeScale = 1f;
            slowMoDuration = 0;
        }
    }

    public void slowMo(float t){
        slowMoDuration = t;
    }
}
