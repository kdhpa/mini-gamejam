using UnityEngine;

public class Planet : MonoBehaviour
{
    public float size;
    public float rotationSpeed;
    public float gravity = 0.98f;

    private Vector3 rotation_dir;
    private Color random_color;
    private MeshRenderer mesh_renderer;
    private void Start()
    {
        RandomDirection();
        //RandomColor();

        mesh_renderer = GetComponent<MeshRenderer>();
        Material mat = mesh_renderer.material;
        //mat.color = 
    }

    private void RandomDirection()
    {
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        rotation_dir = new Vector3(x, y, z);
    }
    private void RandomColor()
    {
        
    }
    private void Update()
    {
        transform.eulerAngles += Vector3.Normalize(rotation_dir) * rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collider_object = other.gameObject;
        if ( collider_object )
        {
            Ship ship = collider_object.GetComponent<Ship>();
        }
    }
}
