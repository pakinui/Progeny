using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResumeCommand : ICommand
{
    public void Execute()
    {
        Time.timeScale = 1;
        GameResumeEvent.Trigger();
    }
}
