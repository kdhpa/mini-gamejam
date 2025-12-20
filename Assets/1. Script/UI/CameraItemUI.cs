using UnityEngine;
using UnityEngine.UI;

public class CameraItemUI : MonoBehaviour
{
    public CameraObject CamObject => camObject;
    private CameraObject camObject;
    private RawImage rawImage;
    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }
    public void SettingCam( CameraObject camera_object )
    {
        if (!camera_object)return; 
        camObject = camera_object;

        if (!rawImage) rawImage = GetComponent<RawImage>();
        rawImage.texture = camObject.TEXTURE;
    }
}
