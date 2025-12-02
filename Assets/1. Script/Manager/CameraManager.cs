using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public List<CameraAttachObject> objects = new List<CameraAttachObject>();
    public List<CameraObject> cam_objects = new List<CameraObject>();
    public RenderInfo[] render_infos;

    public void addObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            objects.Add(attach_obj);
            cam_objects.AddRange(attach_obj.camera_objects);
        }
    }

    public void removeObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            objects.Remove(attach_obj);
            for ( int index = 0; index<attach_obj.camera_objects.Length; index++ )
            {
                cam_objects.Remove(attach_obj.camera_objects[index]);
            }
        }
    }

    public bool isCameraObject( CameraAttachObject attach_obj )
    {
        return attach_obj != null && attach_obj is CameraAttachObject;
    }

    public void ActiveCamera()
    {
        
    }

    public void DeActiveCamera()
    {
        
    }
}


[Serializable]
public struct RenderInfo
{
    public Vector2[] render_size;
    public Vector2[] render_pos;
}
