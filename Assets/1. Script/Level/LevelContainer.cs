using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class LevelContainer : ScriptableObject
{
    public int camera_index = 1;
    public List<LevelObject> level_objects = new List<LevelObject>();

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