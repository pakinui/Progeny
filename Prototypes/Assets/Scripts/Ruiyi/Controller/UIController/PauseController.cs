using Framework;
using Ruiyi.Command;
using Ruiyi.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Ruiyi.UI
{
    public class PauseController : MonoBehaviour, IController
    {
        public Button pauseButton;
        public GameObject pausePanel;
        public Button resumeButton;
        public GameObject resumePanel;
        // Start is called before the first frame update
        private void Awake()
        {
            pauseButton.onClick.AddListener(() => this.SendCommand<GamePauseCommand>());
            resumeButton.onClick.AddListener(() => this.SendCommand<GameResumeCommand>());

            this.RegisterEvent<GamePauseEvent>(e => Pause())
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<GameResumeEvent>(e => Resume())
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Pause()
        {
            Debug.Log("pause");
            //pauseButton.gameObject.SetActive(false);
            //resumeButton.gameObject.SetActive(true);
            pausePanel.SetActive(true);
            // resumePanel.SetActive(false);
        }
      

        private void Resume()
        {
            Debug.Log("resume");
            //pauseButton.gameObject.SetActive(true);
            //resumeButton.gameObject.SetActive(false);
            // resumePanel.SetActive(true);
            pausePanel.SetActive(false);
        }

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
    }
}
    