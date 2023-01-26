using Framework;
using Ruiyi.Event;
using UnityEngine;

namespace Ruiyi.Command
{
    public class GameResumeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Time.timeScale = 1;
            this.SendEvent<GamePauseEvent>();
        }
    }
}
