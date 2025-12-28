using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/ShipObject", fileName = "ShipObject")]
[Serializable]
public class ShipObject : GameableObject, IPathObject
{
    private const string path = "Player"; 
    public string PATH
    {
        get
        {
            return path;
        }
    }
    public int maxGas;
    public int gasSpeed;

    public void Init()
    {
        prefab = Resources.Load<GameObject>(path);
    }
}
