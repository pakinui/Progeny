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
            if (player.GetCurrentHealth() <= 0)
            {
                deathpanel.SetActive(true);
            }
            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !pausePanel.activeSelf)
            {
                Debug.Log("Pause Game");
                pausePanel.SetActive(true);
                //gameObject.SetActive(false);
            }
        }
    }
}