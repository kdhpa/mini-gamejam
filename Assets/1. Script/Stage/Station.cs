using UnityEngine;

public class Station : CameraAttachObject
{
    public float speed = 5;
    public Vector3 direction;
    public Vector3 rotation;

    private void Start()
    {
        this.cameraObjects[0].ActiveSignal(true);
    }

    public void Setting(SpaceStationObject spaceStation)
    {
        speed = spaceStation.speed;
        direction = spaceStation.direction;
        rotation = spaceStation.rotationDirection;
    }

    private void Update()
    {
        if ( direction != Vector3.zero)
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }

        if ( rotation != Vector3.zero)
        {
            this.transform.rotation *= Quaternion.Euler(rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" )
        {
            this.cameraObjects[0].ActiveSignal(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Player" )
        {
            this.cameraObjects[0].ActiveSignal(true);
        }
    }
}
