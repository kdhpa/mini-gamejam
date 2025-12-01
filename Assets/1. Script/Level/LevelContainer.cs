using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class LevelContainer : ScriptableObject
{
    public int camera_index = 1;
    public List<LevelObject> level_objects = new List<LevelObject>();

    public void CreateContainer()
    {
        
    }

    public LevelObject AddObject( GameableObject _object )
    {
        LevelObject lv_object = new LevelObject(_object);
        level_objects.Add( lv_object ); 
        return lv_object;
    }
}

public interface IPathObject
{
    public string PATH {get;}

    public void Init() {}
}

[Serializable]
public class LevelObject 
{
    public Vector3 pos;
    public GameableObject gameable_object;

    public LevelObject()
    {
        
    }

    public LevelObject( GameableObject _object )
    {
        gameable_object = _object;
    }
}

[CreateAssetMenu(menuName = "LevelObject/GameableObject")]
public class GameableObject : ScriptableObject
{
    public GameObject prefab;
}

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

[CreateAssetMenu(menuName = "LevelObject/SpaceStation", fileName = "SpaceStation")]
[Serializable]
public class SpaceStation : GameableObject, IPathObject
{
    private const string path = "Assets/2. Prefab/Space Station.prefab"; 
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