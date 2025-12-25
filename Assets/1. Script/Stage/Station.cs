using UnityEngine;

public class Station : CameraAttachObject
{
    public int speed = 5;
    public Vector3 direction;

    private void Start()
    {
        this.cameraObjects[0].ActiveSignal(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" )
        {
            this.cameraObjects[0].ActiveSignal(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Player" )
        {
            this.cameraObjects[0].ActiveSignal(false);
        }
    }
}
