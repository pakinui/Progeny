using Framework;
using Ruiyi.Event;
using UnityEngine;

namespace Ruiyi.Command
{
    public class GamePauseCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Time.timeScale = 0;
            this.SendEvent<GamePauseEvent>();
        }
    }
}
