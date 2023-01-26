using Framework;
using UnityEngine;

namespace Ruiyi.Controller.GameController
{
    public class FlashController : MonoBehaviour, IController
    {
        private Animator _myAnimator; 
        // Start is called before the first frame update
        private void Awake()
        {
            _myAnimator = GetComponent<Animator>();
            // this.RegisterEvent()
        }

        // Update is called once per frame

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
    }
}
