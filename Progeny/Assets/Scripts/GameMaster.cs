using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour 
{
    private FadeToBlack ftb;
    private CameraController cameraController;

    [SerializeField] private bool changingLevel = false;
    [SerializeField] private bool startingLevel = true;
    [SerializeField] private string nextLevelName;

    private float slowMoDuration;
    public float timeOrder;

    private static GameMaster instance;
    private Player player;
    private PlayerMelee pm;
    public Vector3 lastCheckpointPos;
    
    public string currentLevel = string.Empty;
    
    public void ContinueGame(){
        SceneManager.LoadScene(currentLevel);
    }
    
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
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update(){
        if (changingLevel){
            if (ftb.isBlack){
                changingLevel = false;
                SceneManager.LoadScene(nextLevelName);
                startingLevel = true;
            }
        }
        else if (startingLevel){
            cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            ftb = GameObject.Find("Canvas").GetComponent<FadeToBlack>();
            currentLevel = SceneManager.GetActiveScene().name;
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
        ftb = GameObject.Find("Canvas").GetComponent<FadeToBlack>();
        StartCoroutine(ftb.FadeBlackSquare(true, 0.25f));
        cameraController?.Mute(true, 4f);
        nextLevelName = sceneName;
        changingLevel = true;
        if (SceneManager.GetActiveScene().name != "MainMenu"){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player != null){
                player?.stopPlayerMovement();
            }
        }
    }

    private void StartLevel(){
        ftb.InstantBlack();
        StartCoroutine(ftb.FadeBlackSquare(false, 0.25f));
        cameraController?.Mute(false, 4f);
    }

    public void slowMo(float t){
        slowMoDuration = t;
    }


    public void setCheckpoint(Vector3 pos)
    {
        lastCheckpointPos = pos;
        Debug.Log(pos);
    }


    public Vector3 getLastCheckpoint(){return lastCheckpointPos;}

    
    
    
}
