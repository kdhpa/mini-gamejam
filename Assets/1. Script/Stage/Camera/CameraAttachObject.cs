using System.Collections.Generic;
using UnityEngine;

public class CameraAttachObject : MonoBehaviour
{
    public string id;
    public CameraObject[] cameraObjects;
    public Dictionary<string, CameraObject> cameraDic = new Dictionary<string, CameraObject>();
    protected virtual void Awake()
    {
        cameraObjects = GetComponentsInChildren<CameraObject>();
        for ( int i = 0; i<cameraObjects.Length; i++ )
        {
            CameraObject cam_obj = cameraObjects[i];
            cameraDic[cam_obj.id] = cam_obj;
        }
    }
}
