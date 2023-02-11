using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour 
{
    private FadeToBlack ftb;
    private CameraController cameraController;

    private bool changingLevel = false;
    private bool startingLevel = true;
    private bool readyToStart = true;
    private string nextLevelName;

    private float slowMoDuration;
    public float timeOrder;

    private static GameMaster instance;
    public Vector3 lastCheckpointPos;

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
        ftb = GameObject.Find("Canvas").GetComponent<FadeToBlack>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update(){
        if (changingLevel){
            if (ftb.isBlack){
                changingLevel = false;
                SceneManager.LoadScene(nextLevelName);
                Debug.Log("I HAVE FINALLY LOST VISION1");
                startingLevel = true;
            }
        }
        else if (startingLevel){
            ftb = GameObject.Find("Canvas").GetComponent<FadeToBlack>();
            cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            StartLevel();
            startingLevel = false;
        }
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

    public void NextLevel(string sceneName){
        StartCoroutine(ftb.FadeBlackSquare(true, 0.25f));
        cameraController.Mute(true, 4f);
        nextLevelName = sceneName;
        changingLevel = true;
    }

    private void StartLevel(){
        ftb.InstantBlack();
        StartCoroutine(ftb.FadeBlackSquare(false, 0.25f));
        cameraController.Mute(false, 4f);
    }

    public void slowMo(float t){
        slowMoDuration = t;
    }

    /*
    public void setCheckpoint(Vector2 pos){lastCheckpointPos = pos;}
    public Vector2 getLastCheckpoint(){return lastCheckpointPos;}
    */
}
