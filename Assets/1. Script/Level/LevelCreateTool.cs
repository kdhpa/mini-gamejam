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
    private string resource_path = "Assets/Resources/LevelContainer";

    private List<LevelObject> objects = new List<LevelObject>();
    private List<GameObject> game_objects = new List<GameObject>();

    private string level_container_name = string.Empty;

    private LevelContainer targetSO = null;

    private void OnGUI()
    {
        if (!isStart)
        {
            if (GUILayout.Button("시작"))
            {
                isStart = true;
                container = CreateInstance<LevelContainer>();

                string[] files = Directory.GetFiles(resource_path, "*.asset", SearchOption.AllDirectories);
                int count = files.Length + 1;

                level_container_name = "\\Level" + count + ".asset";
                string a_path = resource_path + level_container_name;

                AssetDatabase.CreateAsset(container, a_path);

                EditorUtility.SetDirty(container);
            }
            
            GUILayout.Space(20);
            GUILayout.Label("레벨 SO", EditorStyles.boldLabel);
        
            // 1. SO를 드래그 앤 드롭 또는 선택할 수 있는 필드
            targetSO = (LevelContainer)EditorGUILayout.ObjectField(
                "Target SO", 
                targetSO, 
                typeof(LevelContainer), 
                false
            );

            if (GUILayout.Button("Load"))
            {
                if (targetSO != null)
                {
                    isStart = true;
                    container = targetSO;
                }
            }
        }

        if (isStart)
        {
            string _path = path + "\\Objects";
            string name = string.Empty;

            if (GUILayout.Button("Planet"))
            {
                PlaneObject pl_object = CreateInstance<PlaneObject>();
                pl_object.Init();
                name = "Plane";

                string secound_path = $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine(_path, secound_path);

                AssetDatabase.CreateAsset(pl_object, combine_path);
                objects.Add(container.AddObject(pl_object));

                game_objects.Add(Instantiate(pl_object.prefab));

                EditorUtility.SetDirty(container);
            }

            if (GUILayout.Button("Magel"))
            {
                MagelObject ma_object = CreateInstance<MagelObject>();
                ma_object.Init();
                name = "Magel";

                string secound_path = $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine(_path, secound_path);

                AssetDatabase.CreateAsset(ma_object, combine_path);
                objects.Add(container.AddObject(ma_object));

                game_objects.Add(Instantiate(ma_object.prefab));

                EditorUtility.SetDirty(container);
            }

            if (GUILayout.Button("SpaceStation"))
            {
                SpaceStationObject sp_object = CreateInstance<SpaceStationObject>();
                sp_object.Init();
                name = "SpaceStation";

                string secound_path = $"{name}{GetFileCount(name)}.asset";
                string combine_path = Path.Combine(_path, secound_path);

                AssetDatabase.CreateAsset(sp_object, combine_path);
                objects.Add(container.AddObject(sp_object));

                game_objects.Add(Instantiate(sp_object.prefab));

                EditorUtility.SetDirty(container);
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Apply"))
            {
                for (int i = 0; i < game_objects.Count; i++)
                {
                    container.level_objects[i].pos = game_objects[i].transform.position;
                }
                EditorUtility.SetDirty(container);
            }

            if (GUILayout.Button("Delete"))
            {
                AssetDatabase.DeleteAsset(resource_path + level_container_name);
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
