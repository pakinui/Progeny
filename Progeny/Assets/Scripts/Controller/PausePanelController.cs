using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
