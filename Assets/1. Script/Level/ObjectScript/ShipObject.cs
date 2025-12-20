using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/ShipObject", fileName = "ShipObject")]
[Serializable]
public class ShipObject : GameableObject, IPathObject
{
    private const string path = "Assets/2. Prefab/Player.prefab"; 
    public string PATH
    {
        get
        {
            return path;
        }
    }
    public int maxGas;

    public void Init()
    {
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }
}
