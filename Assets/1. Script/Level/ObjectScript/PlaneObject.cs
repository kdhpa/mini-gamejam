using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/PlaneObject")]
[Serializable]
public class PlaneObject : GameableObject, IPathObject
{
    public Vector3 size;
    public MagelObject magel_object;

    public float rotSpeed;
    public float revSpeed;

    public Vector3 revDir;
    public Vector3 rotDir;

    protected const string path = "Planet"; 
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