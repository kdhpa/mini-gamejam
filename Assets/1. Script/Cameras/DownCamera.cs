using System;
using TMPro;
using UnityEngine;

public class DownCamera : PlayerCamera
{
    [SerializeField]
    private TextMeshProUGUI percentageMeshPro;

    protected override void Start()
    {
        base.Start();
        EventManager.Instance.AddEventListner<DockPercentEventArgs>("DockPercent",DockPercent);
        EventManager.Instance.AddEventListner("Dock",DockEvent);

        percentageMeshPro.gameObject.SetActive(false);
    }

    private void DockEvent(object sender , EventArgs dock_percent)
    {
        if (dock_percent != EventArgs.Empty)
        {
            percentageMeshPro.gameObject.SetActive(true);
        }
        else
        {
            percentageMeshPro.gameObject.SetActive(false);
        }
    }

    private void DockPercent(object sender , DockPercentEventArgs dock_percent)
    {
        percentageMeshPro.text = Math.Round(dock_percent.dockPercent, 2) + "%";
    }
}
