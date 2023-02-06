using UnityEngine;

namespace Controller
{
    public class GamePanelController : MonoBehaviour
    {
        public Player player;
        public GameObject deathpanel;
        public GameObject pausePanel;
        void Start()
        {
            // assigning references
            
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Quit Game");
                Application.Quit();
            }
            if (player.GetCurrentHealth() <= 0)
            {
                deathpanel.SetActive(true);
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