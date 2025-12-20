using UnityEngine;

public class CameraObject : MonoBehaviour
{
    protected Camera cam;
    protected RenderTexture renderTexture;
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

    public void Awake()
    {
        cam = GetComponent<Camera>();

        var desc = new RenderTextureDescriptor(1920, 1080)
        {
            colorFormat = RenderTextureFormat.ARGB32,
            depthBufferBits = 16,
            msaaSamples = 1,    
            dimension = UnityEngine.Rendering.TextureDimension.Tex2D
        };
        renderTexture = new RenderTexture(desc);
        cam.targetTexture = renderTexture;
    }
}
