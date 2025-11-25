using UnityEngine;

public interface IMainCam
{
    public Camera CAM {get;}
}

public interface ISubCam
{
    public Camera Cam {get;}
    public Vector2 Area {get;set;}
    public int Priority{get; set;}
}


public class Maggel : MonoBehaviour, IMainCam
{
    public Camera CAM
    {
        get { return camera; }
    }

    [SerializeField]
    private Camera camera;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
