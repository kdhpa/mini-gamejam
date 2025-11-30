using UnityEditor;
using UnityEngine;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

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

    private List<LevelObject> objects = new List<LevelObject>();
    private List<GameObject> game_objects = new List<GameObject>();

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
                pl_object.Init();
                name = "Plane";

                string secound_path = $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine( _path, secound_path );

                AssetDatabase.CreateAsset(pl_object, combine_path);
                objects.Add(container.AddObject( pl_object ));

                game_objects.Add(Instantiate(pl_object.prefab));
            }

            if ( GUILayout.Button("Magel") )
            {
                MagelObject ma_object = CreateInstance<MagelObject>();
                ma_object.Init();
                name = "Magel";

                string secound_path =  $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine( _path, secound_path );

                AssetDatabase.CreateAsset(ma_object, combine_path);
                objects.Add(container.AddObject( ma_object ));

                game_objects.Add(Instantiate(ma_object.prefab));
            }

            if ( GUILayout.Button("SpaceStation") )
            {
                SpaceStation sp_object=  CreateInstance<SpaceStation>();
                sp_object.Init();
                name = "SpaceStation";

                string secound_path =  $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine( _path, secound_path );

                AssetDatabase.CreateAsset( sp_object, combine_path );
                objects.Add(container.AddObject( sp_object ));

                game_objects.Add(Instantiate(sp_object.prefab));
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Delete"))
            {
                
            }
        }

    }

    private int GetFileCount( string name )
    {
        string[] files = Directory.GetFiles(path, $"{name}*.asset", SearchOption.AllDirectories);
        int count = files.Length + 1;

        return count;
    }

    private void OnInspectorUpdate()
    {
        
    }
}
