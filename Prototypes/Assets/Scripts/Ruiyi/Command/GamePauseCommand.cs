using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseCommand : ICommand
{
    public void Execute()
    {
        Time.timeScale = 0;
        GamePauseEvent.Trigger();
    }
}
