using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pauseMenu;
    public Button resumeButton;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseButton.onClick.AddListener(() =>
        {
            
            new GamePauseCommand().Execute();
        });

        resumeButton.onClick.AddListener(() =>
        {
            Debug.Log("resume");
            new GameResumeCommand().Execute();
        });


        GamePauseEvent.Resgister(Pause);
        GameResumeEvent.Resgister(Resume);

    }

    private void Pause()
    {
        Debug.Log("pause begin");
        pauseButton.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
        Debug.Log("pause end");
    }
      

    private void Resume()
    {
        pauseButton.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        GamePauseEvent.Delete(Pause);
        GameResumeEvent.Delete(Resume);
    }
}
    