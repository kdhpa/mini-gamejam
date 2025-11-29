using UnityEditor;
using UnityEngine;
using System.IO;
using NUnit.Framework;

public class LevelCreateTool : EditorWindow
{
    [MenuItem("CustomTool/LevelCreateTool")]
    static void CustomEditor()
    {
        GetWindow<LevelCreateTool>();
    }


    private LevelContainer container;
    private bool isStart = false;
    private string path = "Assets\\1. Script\\Level";

    private LevelObject[] objects;

    private void OnGUI()
    {
        if (!isStart)
        {
            if ( GUILayout.Button("시작") )
            {
                isStart = true;
                container = CreateInstance<LevelContainer>();

                string folder_path = path + "\\Levels";

                string[] files = Directory.GetFiles(folder_path, "*.cs", SearchOption.AllDirectories);
                int count = files.Length + 1;
                
                string a_path = folder_path + "\\Level" + count + ".asset";

                AssetDatabase.CreateAsset(container, a_path);
                container.CreateContainer();
            }
        }

        if ( isStart )
        {
            string _path = path + "\\Objects";
            string name = string.Empty;
            
            if ( GUILayout.Button("Planet") )
            {
                PlaneObject pl_object = CreateInstance<PlaneObject>();
                name = "\\Plane";
                name = name + GetFileCount(name) + ".asset";
                _path += name;

                AssetDatabase.CreateAsset(pl_object, _path);
                objects[objects.Length + 1] = container.AddObject( pl_object );
            }

            if ( GUILayout.Button("Magel") )
            {
                MagelObject ma_object = CreateInstance<MagelObject>();
                name = "\\Magel";
                name = name + GetFileCount(name) + ".asset";
                _path += name;

                AssetDatabase.CreateAsset(ma_object, _path);
                objects[objects.Length + 1] = container.AddObject( ma_object );
            }

            if ( GUILayout.Button("SpaceStation") )
            {
                MagelObject ma_object = CreateInstance<MagelObject>();
                name = "\\SpaceStation";
                name = name + GetFileCount(name) + ".asset";
                _path += name;

                AssetDatabase.CreateAsset(ma_object, _path);
                objects[objects.Length + 1] = container.AddObject( ma_object );
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Delete"))
            {
                
            }
        }

    }

    private int GetFileCount( string name )
    {
        string[] files = Directory.GetFiles(path, name+"*.asset", SearchOption.AllDirectories);
        int count = files.Length + 1;

        return count;
    }

    private void OnInspectorUpdate()
    {
        
    }
}
