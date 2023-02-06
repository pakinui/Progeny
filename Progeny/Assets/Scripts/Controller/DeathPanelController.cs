using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelController : MonoBehaviour
{
    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
