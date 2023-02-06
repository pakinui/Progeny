using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour 
{
    private float slowMoDuration;
    public float timeOrder;

    private static GameMaster instance;
    private Vector2 lastCheckpointPos;

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(gameObject);
        }
    }

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

    public void checkpoint(Vector2 pos){
        lastCheckpointPos = pos;
    }
}
