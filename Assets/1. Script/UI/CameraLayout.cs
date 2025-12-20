using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLayout : MonoBehaviour
{
    [SerializeField]
    private LayoutType layoutType;
    public LayoutType LayoutType => layoutType;
    private CameraScreen[] camScreens;

    public List<CameraItemUI> CamItems => camItems;
    private List<CameraItemUI> camItems = new List<CameraItemUI>();

    private void Awake()
    {
        camScreens = this.GetComponentsInChildren<CameraScreen>();
    }
    public void ChangeScene( int index , CameraItemUI camItemUI )
    {
        camItems.Add(camItemUI);
        camScreens[index].Screen(camItemUI);
    }
}

public enum LayoutType
{
    Tab,
    Basic
}