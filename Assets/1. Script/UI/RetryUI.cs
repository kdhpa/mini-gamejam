using System;
using UnityEngine;

public class RetryUI : EndUI
{
    public void GoToRetry()
    {
        EventManager.Instance.AllClear();
        LevelSystem.Instance.Restart();
    }
}
