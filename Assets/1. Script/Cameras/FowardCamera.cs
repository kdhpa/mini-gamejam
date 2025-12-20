using UnityEngine;
using UnityEngine.UI;

public class FowardCamera : CameraObject
{
    private Slider gasSlider;
    private void Start()
    {
        gasSlider = GetComponent<Slider>();
    }

    private void GasChange(float slider)
    {
        
    }
}
