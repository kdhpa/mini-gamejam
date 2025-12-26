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

    public void GoToNextLevel()
    {
        if (!LevelSystem.Instance.IsNextable()) return;
        LevelSystem.Instance.StartLevel(LevelSystem.Instance.curIndex + 1);
    }

    protected override void End(object sender, EventArgs eventArgs)
    {
        Ship ship = FindAnyObjectByType<Ship>();
        ChangeResult(ship.gas, ship.maxGas);
        base.End(sender, eventArgs);
    }

    public void ChangeResult(float gas, int maxGas)
    {
        float gasPercent = maxGas / gas;
        ChangeGrade(gasPercent);
        ChageScore(gasPercent);
    }

    public void ChageScore(float gasPercent)
    {
        int finScore = (int)(gasPercent * Math.Pow(10, 7));
        ScoreText.text = finScore.ToString();

    }

    public void ChangeGrade(float gasPercent)
    {
        if (gasPercent >= 0.5)
        {
            GradeText.color = GradeColors[0];
            GradeText.text = "A";
        }
        else if (gasPercent >= 0.3)
        {
            GradeText.color = GradeColors[1];
            GradeText.text = "B";
        }
        else if (gasPercent >= 0.2)
        {
            GradeText.color = GradeColors[2];
            GradeText.text = "C";
        }
    }
}
