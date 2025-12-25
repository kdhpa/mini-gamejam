using System;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FowardCamera : PlayerCamera
{
    private Slider gasSlider;

    [SerializeField]
    private TextMeshProUGUI xTextMeshPro;
    [SerializeField]
    private TextMeshProUGUI yTextMeshPro;
    [SerializeField]
    private TextMeshProUGUI zTextMeshPro;

    [SerializeField]
    private TextMeshProUGUI xRotateMeshPro;
    [SerializeField]
    private TextMeshProUGUI yRotateMeshPro;

    protected override void Start()
    {
        base.Start();
        gasSlider = GetComponentInChildren<Slider>();
        EventManager.Instance.AddEventListner<GasChangeEventArgs>("GasChange",GasChange);
        EventManager.Instance.AddEventListner<AxisChangeEventArgs>("AxisChange",AxisChange);
        EventManager.Instance.AddEventListner<AxisChangeEventArgs>("RotateChange",RotateChange);
    }

    private void GasChange(object sender, GasChangeEventArgs change_args)
    {
        if (change_args == null) return;
        gasSlider.maxValue = change_args.MaxGas;
        gasSlider.value = change_args.CurrentGas;
    }

    private void AxisChange(object sender, AxisChangeEventArgs change_args)
    {
        if (change_args == null) return;
        if (change_args.axis == null) return;

        Vector3 axis = change_args.axis;
        xTextMeshPro.text = "X Axis : " + Math.Round(axis.x, 2);
        yTextMeshPro.text = "Y Axis : " + Math.Round(axis.y, 2);
        zTextMeshPro.text = "Z Axis : " + Math.Round(axis.z, 2); 
    }

    private void RotateChange(object sender, AxisChangeEventArgs change_args)
    {
        if (change_args == null) return;
        if (change_args.axis == null) return;

        Vector3 axis = change_args.axis;
        xRotateMeshPro.text = "X Rotate : " + Math.Round(axis.x, 2);
        yRotateMeshPro.text = "Y Rotate : " + Math.Round(axis.y, 2);
    }
}
