using System;
using System.Collections;
using UnityEngine;

public class ClearUI : EndUI
{
    [SerializeField]
    private GameObject clearPanel;

    [SerializeField]
    private GameObject nextButton;

    public void OnEnable()
    {
        nextButton.SetActive(LevelSystem.Instance.IsNextable());
    }

    public void GoToNextLevel()
    {
        EventManager.Instance.AllClear();
        LevelSystem.Instance.NextLevel();
        clearPanel.SetActive(false);
    }
}
