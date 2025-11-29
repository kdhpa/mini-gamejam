using System;
using UnityEngine;

[CreateAssetMenu]
public class LevelContainer : ScriptableObject
{
    public int camera_index = 1;
    public LevelObject[] level_objects;

    public void CreateContainer()
    {
        //level_objects[1] = 
    }

    public LevelObject AddObject( GameableObject _object )
    {
        int length = level_objects.Length;
        level_objects[length] = new LevelObject(_object); 

        return level_objects[length];
    }
}

public interface IPathObject
{
    public string PATH {get;}
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
}

