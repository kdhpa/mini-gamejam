using UnityEngine;
using UnityEngine.UI;

public class CameraItemUI : MonoBehaviour
{
    public CameraObject CamObject => camObject;
    private CameraObject camObject;

    private Image selectImage;
    private GameObject selectObject;

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
        if (!selectImage)
        {
            selectImage = GetComponentInChildren<Image>();
            selectObject = selectImage.gameObject;
            Select(false);
        }
        rawImage.texture = camObject.TEXTURE;
    }

    public void Select(bool flag)
    {
        if (!selectObject) return;
        selectObject.SetActive(flag);
    }
}
