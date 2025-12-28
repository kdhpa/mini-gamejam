using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "LevelObject/SpaceStation", fileName = "SpaceStation")]
[Serializable]
public class SpaceStationObject : GameableObject, IPathObject
{
    private const string path = "Space Station"; 
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
        prefab = Resources.Load<GameObject>(path);
    }
}
