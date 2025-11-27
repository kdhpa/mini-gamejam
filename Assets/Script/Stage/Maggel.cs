using UnityEngine;


public class Maggel : CameraAttachObject
{
    void Awake()
    {
        camera_objects = GetComponentsInChildren<CameraObject>();
    }
}
