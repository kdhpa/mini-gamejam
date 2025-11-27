using System;
using Unity.VisualScripting;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public CameraAttachObject[] objects;
    public RenderInfo[] render_infos;
    private void Start()
    {
        
    }
}


[Serializable]
public struct RenderInfo
{
    public Vector2[] render_size;
    public Vector2[] render_pos;
}
