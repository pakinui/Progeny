using UnityEngine;

namespace Controller
{
    public class GamePanelController : MonoBehaviour
    {
        public GameObject pausePanel;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Quit Game");
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Pause Game");
                pausePanel.SetActive(true);
                //gameObject.SetActive(false);
            }
        }
    }
}