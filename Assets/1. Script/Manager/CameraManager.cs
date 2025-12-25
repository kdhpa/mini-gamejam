using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private List<CameraAttachObject> attach_objects = new List<CameraAttachObject>();
    private List<CameraObject> cam_objects = new List<CameraObject>();
    public RenderInfo[] render_infos;

    public List<CameraObject> CAM_OBJECTS
    {
        get
        {
            return cam_objects;
        }
    }

    public void addObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            attach_objects.Add(attach_obj);
            CameraObject[] camcam_ojects = attach_obj.cameraObjects;
            cam_objects.AddRange(camcam_ojects);
        }
    }

    public void removeObject( CameraAttachObject attach_obj )
    {
        if ( isCameraObject(attach_obj) )
        {
            attach_objects.Remove(attach_obj);
            for ( int index = 0; index< attach_obj.cameraObjects.Length; index++ )
            {
                cam_objects.Remove(attach_obj.cameraObjects[index]);
            }
        }
    }

    public void clear()
    {
        attach_objects.Clear();
        cam_objects.Clear();
    }

    public bool isCameraObject( CameraAttachObject attach_obj )
    {
        return attach_obj != null && attach_obj is CameraAttachObject;
    }

    public void ActiveCamera( int index )
    {
        int render_index = LevelSystem.Instance.CurContainer.camera_index;

        if (cam_objects.Count <= index ) return;

        CameraObject cam_object = cam_objects[index];
        Camera camera = cam_object.CAMERA;
        camera.gameObject.SetActive(true);
    }

    public void DeActiveCamera(int cam_index )
    {
        CameraObject cam_object = cam_objects[cam_index];
        Camera camera = cam_object.CAMERA;
        camera.gameObject.SetActive(false);
    }
}


[Serializable]
public struct RenderInfo
{
    public Vector2[] render_size;
    public Vector2[] render_pos;
}