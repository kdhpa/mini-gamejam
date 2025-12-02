using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/MagelObject", fileName = "MagelObject")]
[Serializable]
public class MagelObject : GameableObject, IPathObject
{
    private const string path = "Assets/2. Prefab/Maggel.prefab"; 
    public string PATH
    {
        get
        {
            return path;
        }
    }

    public void Init()
    {
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }
}
