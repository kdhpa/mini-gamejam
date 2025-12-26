using System;
using System.Collections.Generic;
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

    // 0 : forawrd, 1 : back, 2 : left, 3 : right, 4 : up, 5 : down
    [SerializeField]
    private List<Image> InputCheckImages;

    protected override void Start()
    {
        base.Start();
        gasSlider = GetComponentInChildren<Slider>();
        EventManager.Instance.AddEventListner<GasChangeEventArgs>("GasChange", GasChange);
        EventManager.Instance.AddEventListner<AxisChangeEventArgs>("AxisChange", AxisChange);
        EventManager.Instance.AddEventListner<AxisChangeEventArgs>("RotateChange", RotateChange);
        EventManager.Instance.AddEventListner<AxisChangeEventArgs>("InputChange", InputChange);
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

    private void InputChange(object sender, AxisChangeEventArgs change_args)
    {
        Vector3 inputVec = change_args.axis;

        // forward / back input check
        bool isInput_z = inputVec.z > 0;
        bool isNonInput_z = inputVec.z != 0;
        InputCheckImages[0].gameObject.SetActive(isInput_z & isNonInput_z);
        InputCheckImages[1].gameObject.SetActive(!isInput_z & isNonInput_z);
        
        // left / right input check
        bool isInput_x = inputVec.x > 0;
        bool isNonInput_x = inputVec.x != 0;
        InputCheckImages[3].gameObject.SetActive(isInput_x & isNonInput_x);
        InputCheckImages[2].gameObject.SetActive(!isInput_x & isNonInput_x);

        // up / down input check
        bool isInput_y = inputVec.y > 0;
        bool isNonInput_y = inputVec.y != 0;
        InputCheckImages[4].gameObject.SetActive(isInput_y & isNonInput_y);
        InputCheckImages[5].gameObject.SetActive(!isInput_y & isNonInput_y);
    }
}