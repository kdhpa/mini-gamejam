using System;
using System.Collections;
using UnityEngine;

public class ClearUI : EndUI
{
    public void GoToNextLevel()
    {
        if(!LevelSystem.Instance.IsNextable()) return;
        LevelSystem.Instance.StartLevel(LevelSystem.Instance.curIndex + 1);
    }
}
