using System;
using UnityEngine;
using UnityEngine.UI;

public class FowardCamera : PlayerCamera
{
    private Slider gasSlider;

    protected override void Start()
    {
        base.Start();
        gasSlider = GetComponentInChildren<Slider>();
        EventManager.Instance.AddEventListner<GasChangeEventArgs>("GasChange",GasChange);
    }

    private void GasChange(object sender, GasChangeEventArgs change_args)
    {
        if (change_args == null) return;
        gasSlider.maxValue = change_args.MaxGas;
        gasSlider.value = change_args.CurrentGas;
    }
}
