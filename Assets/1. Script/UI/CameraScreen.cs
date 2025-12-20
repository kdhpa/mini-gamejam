using UnityEngine;
using UnityEngine.UI;

public class CameraScreen : MonoBehaviour
{
    private RawImage rawIamge;

    private void Awake()
    {
        rawIamge = GetComponent<RawImage>();
    }

    public void Screen(CameraItemUI camItemUI)
    {
        if (!camItemUI) return;

        rawIamge.texture = camItemUI.CamObject.TEXTURE;
        rawIamge.uvRect = new Rect(0,0,1,1);
    }
}
