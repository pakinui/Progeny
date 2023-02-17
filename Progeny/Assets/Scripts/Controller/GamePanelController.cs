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
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void Update()
        {
            if (player.GetCurrentHealth() < 0 )
            {
                Debug.Log("dying game panel");
                deathpanel.SetActive(true);
            }else{
                deathpanel.SetActive(false);
            }
            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !pausePanel.activeSelf)
            {
                //Debug.Log("Pause Game");
                pausePanel.SetActive(true);
                //gameObject.SetActive(false);
            }
        }
    }
}