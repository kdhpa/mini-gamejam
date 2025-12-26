using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/SpaceStation", fileName = "SpaceStation")]
[Serializable]
public class SpaceStationObject : GameableObject, IPathObject
{
    private const string path = "Assets/2. Prefab/Space Station.prefab"; 
    public string PATH
    {
        get
        {
            return path;
        }
    }
    
    public Vector3 direction = Vector3.zero;
    public Vector3 rotationDirection = Vector3.zero;
    public float speed = 0f;

    public void Init()
    {
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }
}
