using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
