using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/MagelObject", fileName = "MagelObject")]
[Serializable]
public class MagelObject : GameableObject, IPathObject
{
    private const string path = "Maggel"; 
    public string PATH
    {
        get
        {
            return path;
        }
    }

    public void Init()
    {
        prefab = Resources.Load<GameObject>(path);
    }
}
