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

    private bool isTab = false;

    private void Awake()
    {
        camScrollView = GetComponentInChildren<CamSwitchScrollView>();
        playerInput = GetComponent<PlayerInput>();
        camLayouts = GetComponentsInChildren<CameraLayout>();

        tab_action = playerInput.actions.FindAction("Tab");
        tab_action.performed += Tab;

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

        int count = camScrollView.CAMITEMS.Count < 4 ? camScrollView.CAMITEMS.Count : 4;
        for ( int i = 0; i < count; i++ )
        {
            currentLayout.ChangeScene( i, camScrollView.CAMITEMS[i] );
        }
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
