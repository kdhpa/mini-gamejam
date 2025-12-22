using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraUI : MonoBehaviour
{
    private CamSwitchScrollView camScrollView;
    private CameraLayout[] camLayouts;
    private CameraLayout currentLayout;
    private Dictionary<LayoutType, CameraLayout> dicLayout = new Dictionary<LayoutType, CameraLayout>();
    private PlayerInput playerInput;
    private InputAction tab_action;
    private InputAction up_action;
    private InputAction down_action;

    private InputAction one_action;
    private InputAction two_action;
    private InputAction th_action;
    private InputAction four_action;

    private bool isTab = false;

    private void Awake()
    {
        camScrollView = GetComponentInChildren<CamSwitchScrollView>();
        playerInput = GetComponent<PlayerInput>();
        camLayouts = GetComponentsInChildren<CameraLayout>();

        tab_action = playerInput.actions.FindAction("Tab");
        up_action = playerInput.actions.FindAction("Up");
        down_action = playerInput.actions.FindAction("Down");

        one_action = playerInput.actions.FindAction("1");
        two_action = playerInput.actions.FindAction("2");
        th_action = playerInput.actions.FindAction("3");
        four_action = playerInput.actions.FindAction("4");

        tab_action.performed += Tab;
        
        up_action.performed += Up;
        down_action.performed += Down;

        one_action.performed += One;
        two_action.performed += Two;
        th_action.performed += Three;
        four_action.performed += Four;

        for ( int i = 0; i < camLayouts.Length; i++ )
        {
            LayoutType layout_type = camLayouts[i].LayoutType;
            dicLayout.Add( layout_type, camLayouts[i] );
        }

        LayoutVisible(isTab);
    }

    private void Start()
    {
        camScrollView.initScrollView();

        List<CameraItemUI> camItemUIs = new List<CameraItemUI>();
        for ( int i = 0; i < camScrollView.CAMITEMS.Count; i++ )
        {
            if ( i > 4 ) return;
            camItemUIs.Add(camScrollView.CAMITEMS[i]);
        }
        currentLayout.Setting(camItemUIs);
    }

    private void Tab( InputAction.CallbackContext callback_context )
    {
        if ( isTab )
        {
            isTab = false;
            LayoutVisible(isTab);
        }
        else
        {
            isTab = true;
            LayoutVisible(isTab);
        }
    }

    private void Up(InputAction.CallbackContext callbackContext)
    {
        camScrollView.Select(1);
    }

    private void Down(InputAction.CallbackContext callbackContext)
    {
        camScrollView.Select(-1);
    }
    
    private void One(InputAction.CallbackContext callbackContext)
    {
        if(!isTab) return;
        currentLayout.ChangeScene(0, camScrollView.CURRENTITEM);
    }

    private void Two(InputAction.CallbackContext callbackContext)
    {
        if(!isTab) return;
        currentLayout.ChangeScene(1, camScrollView.CURRENTITEM);
    }

    private void Three(InputAction.CallbackContext callbackContext)
    {
        if(!isTab) return;
        currentLayout.ChangeScene(2, camScrollView.CURRENTITEM);
    }

    private void Four(InputAction.CallbackContext callbackContext)
    {
        if(!isTab) return;
        currentLayout.ChangeScene(3, camScrollView.CURRENTITEM);
    }
    private void LayoutVisible( bool is_active )
    {
        CameraLayout tabLayout = dicLayout[LayoutType.Tab];
        CameraLayout basicLayout = dicLayout[LayoutType.Basic];

        if (!tabLayout) return;
        if (!basicLayout) return;

        GameObject tabObject = tabLayout.gameObject;
        GameObject basicObject = basicLayout.gameObject;

        if (!tabObject) return;
        if (!basicLayout) return;

        tabObject.SetActive(is_active);
        basicObject.SetActive(!is_active);

        currentLayout = is_active ? tabLayout : basicLayout;
    }
}
