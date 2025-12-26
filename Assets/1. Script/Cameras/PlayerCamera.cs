using System;
using UnityEngine;

public class PlayerCamera : CameraObject
{
    protected virtual void Start()
    {
        EventManager.Instance.AddEventListner("Fail",DestoryEvent);
    }
    protected virtual void DestoryEvent(object sender, EventArgs args)
    {
        ActiveSignal(true);
        cam.cullingMask = LayerMask.GetMask("UI");
    }
}