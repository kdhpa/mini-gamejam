using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CamSwitchScrollView : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject prefab;
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<CameraItemUI> camItemUIs = new List<CameraItemUI>();

    public List<CameraItemUI> CAMITEMS => camItemUIs;

    public void initScrollView()
    {
        List<CameraObject> camera_object = CameraManager.Instance.CAM_OBJECTS;
        for ( int i = 0; i < camera_object.Count; i++ )
        {
            GameObject game_object = Instantiate(prefab , rectTransform);
            gameObjects.Add(game_object);
            
            CameraItemUI camera_item_ui = game_object.GetComponent<CameraItemUI>();
            camera_item_ui.SettingCam(camera_object[i]);

            camItemUIs.Add(camera_item_ui);
        }
    }
}
