using UnityEngine;

public class Station : CameraAttachObject
{
    public int speed = 5;
    public Vector3 direction;
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" )
        {
            
        }
    }
}
