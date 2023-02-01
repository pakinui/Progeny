using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    public GameObject optionPanel;
    
    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene("Level1Art");
    }
    
    public void OptionGame()
    {
        Debug.Log("Option Game");
        optionPanel.SetActive(true);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
