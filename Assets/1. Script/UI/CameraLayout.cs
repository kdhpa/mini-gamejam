using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CameraLayout : MonoBehaviour
{
    [SerializeField]
    private LayoutType layoutType;
    public LayoutType LayoutType => layoutType;
    private CameraScreen[] camScreens;

    private CameraItemUI[] screenItems = new CameraItemUI[4];
    public CameraItemUI[] ScreenItems => screenItems;

    private void Awake()
    {
        camScreens = this.GetComponentsInChildren<CameraScreen>();
    }

    public void Setting(CameraItemUI[] cameraItems )
    {
        screenItems = cameraItems;
        for (int i = 0; i<4; i++)
        {
            camScreens[i].Screen(screenItems[i]);
        }
    }
    public void ChangeScene( int index , CameraItemUI camItemUI )
    {
        CameraItemUI prev_item = screenItems[index];
        for (int i = 0; i < 4; i++)
        {
            CameraItemUI index_item = screenItems[i];
            CameraObject cam_object = index_item.CamObject;
            if (i != index && cam_object.id == camItemUI.CamObject.id)
            {
                screenItems[i] = prev_item;
                screenItems[index] = camItemUI;

                camScreens[i].Screen(prev_item);
                camScreens[index].Screen(camItemUI);
                return;
            }
        }
        camScreens[index].Screen(camItemUI);
    }
}

public enum LayoutType
{
    Tab,
    Basic
}