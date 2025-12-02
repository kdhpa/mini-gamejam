using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/PlaneObject")]
[Serializable]
public class PlaneObject : GameableObject, IPathObject
{
    public Vector3 size;
    public bool is_size_random;
    public MagelObject magel_object;

    protected const string path = "Assets/2. Prefab/Planet.prefab"; 
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