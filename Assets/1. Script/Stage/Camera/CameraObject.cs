using UnityEngine;
using UnityEngine.UI;

public class CameraObject : MonoBehaviour, IPathObject
{
    protected Camera cam;
    protected RenderTexture renderTexture;      // 최종 출력 (UI에 표시)
    protected GameObject noSignalPanel;
    protected Canvas canvas;
    protected const string prefab_path = "NoSignalPanel";
    public string PATH
    {
        get
        {
            return prefab_path;
        }
    }
    public Camera CAMERA { get {
        return cam;
    } }

    public RenderTexture TEXTURE
    {
        get
        {
            return renderTexture;
        }
    }

    public string id = string.Empty;

    protected virtual void Awake()
    {
        cam = GetComponent<Camera>();
        canvas = GetComponentInChildren<Canvas>();

        if (!canvas)
        {
            GameObject canvasObj = new("Canvas");
            canvasObj.transform.SetParent(transform, false);
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cam;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        var desc = new RenderTextureDescriptor(1920, 1080)
        {
            colorFormat = RenderTextureFormat.ARGB32,
            depthBufferBits = 16,
            msaaSamples = 1,
            dimension = UnityEngine.Rendering.TextureDimension.Tex2D
        };

        renderTexture = new RenderTexture(desc);
        renderTexture.Create();

        cam.targetTexture = renderTexture;

        GameObject prefab = Resources.Load<GameObject>(prefab_path);
        if (prefab != null && canvas != null)
        {
            noSignalPanel = Instantiate(prefab);
            noSignalPanel.transform.SetParent(canvas.transform, false);
            ActiveSignal(false);
        }
        else
        {
            Debug.Log($"CameraObject: prefab={prefab != null}, canvas={canvas != null}");
        }
    }

    public void ActiveSignal(bool isActive)
    {
        if (noSignalPanel == null) return;
        noSignalPanel.SetActive(isActive);
    }
}
