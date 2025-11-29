using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<CameraAttachObject> objects = new List<CameraAttachObject>();
    public RenderInfo[] render_infos;

    public void addObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            objects.Add(attach_obj);
        }
    }

    public void removeObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            objects.Remove(attach_obj);
        }
    }

    public bool isCameraObject( CameraAttachObject attach_obj )
    {
        return attach_obj != null && attach_obj is CameraAttachObject;
    }
}


[Serializable]
public struct RenderInfo
{
    public Vector2[] render_size;
    public Vector2[] render_pos;
}
