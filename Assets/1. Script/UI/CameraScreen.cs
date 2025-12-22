using UnityEngine;
using UnityEngine.UI;

public class CameraScreen : MonoBehaviour
{
    private RawImage rawIamge;
    private CameraItemUI camItem;

    public CameraItemUI CAMITEM => camItem;

    private void Awake()
    {
        rawIamge = GetComponent<RawImage>();
    }

    public void Screen(CameraItemUI camItemUI)
    {
        if (!camItemUI) return;

        camItem = camItemUI;

        rawIamge.texture = camItemUI.CamObject.TEXTURE;
        rawIamge.uvRect = new Rect(0,0,1,1);
    }
}
