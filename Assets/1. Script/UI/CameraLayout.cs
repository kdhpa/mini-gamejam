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

    private void Awake()
    {
        camScreens = this.GetComponentsInChildren<CameraScreen>();
    }

    public void Setting(List<CameraItemUI> cameraItems )
    {
        screenItems = cameraItems.ToArray();
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
            if (screenItems[i] == camItemUI)
            {
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