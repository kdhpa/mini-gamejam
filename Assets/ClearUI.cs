using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearUI : EndUI
{
    [SerializeField]
    private TextMeshProUGUI GradeText;

    // 0 : A, 1 : B, 2 : C
    [SerializeField]
    private List<Color> GradeColors;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

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

    protected override void End(object sender, EventArgs eventArgs)
    {
        Ship ship = FindAnyObjectByType<Ship>();
        ChangeResult(ship.gas, ship.maxGas);
        base.End(sender, eventArgs);
    }

    public void ChangeResult(float gas, int maxGas)
    {
        float gasPercent = gas / maxGas;
        ChangeGrade(gasPercent);
        ChageScore(gasPercent);
    }

    public void ChageScore(float gasPercent)
    {
        int finScore = (int)(gasPercent * Math.Pow(10, 7))*2;
        ScoreText.text = finScore.ToString();
        
        int length = ScoreText.text.Length;
        for(int i = 8-length; i > 0; i--)
        {
            ScoreText.text = "0" + ScoreText.text;
        }
    }

    public void ChangeGrade(float gasPercent)
    {
        if (gasPercent >= 0.5f)
        {
            GradeText.color = GradeColors[0];
            GradeText.text = "A";
        }
        else if (gasPercent >= 0.25f)
        {
            GradeText.color = GradeColors[1];
            GradeText.text = "B";
        }
        else
        {
            GradeText.color = GradeColors[2];
            GradeText.text = "C";
        }
    }
}
