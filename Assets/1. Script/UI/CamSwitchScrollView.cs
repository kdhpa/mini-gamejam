using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamSwitchScrollView : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject prefab;
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<CameraItemUI> camItemUIs = new List<CameraItemUI>();
    private CameraItemUI currentItem;
    private int curIndex = 0;
    private int maxIndex = 0;

    private ScrollRect scrollRect;
    private RectTransform content;

    public List<CameraItemUI> CAMITEMS => camItemUIs;
    public CameraItemUI CURRENTITEM => currentItem;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
    }

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
            maxIndex++;
        }

        if (!currentItem)
        {
            currentItem = camItemUIs[0];
            SelectItem();
        }
    }

    public void Select( int direction )
    {
        int max_index = maxIndex - 1;
        if ( curIndex + direction < 0 )
        {
            curIndex = max_index;
            SelectItem();
            content.anchoredPosition = new Vector2(0, 104 * max_index);
            return;
        }

        if ( curIndex + direction > max_index )
        {
            curIndex = 0;
            SelectItem();
            content.anchoredPosition = new Vector3(0, 0, 0);
            return;
        }

        curIndex = curIndex + direction;
        content.anchoredPosition = new Vector3(0, 104 * curIndex + 20 * curIndex, 0);
        SelectItem();
    }

    private void SelectItem()
    {
        if ( camItemUIs.Count < curIndex || curIndex < 0  )
            return;

        if (currentItem)
        {
            //prev초기화
            currentItem.Select(false);
        }
        
        currentItem = camItemUIs[curIndex];
        if (currentItem)
        {
            currentItem.Select(true);
        }
    }
}
