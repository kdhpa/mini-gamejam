using UnityEditor;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public string url = string.Empty;
    private LevelContainer[] containers;

    private void Awake()
    {
        containers = Resources.LoadAll<LevelContainer>("Assets/1. Script/Level/Levels");
    }

    private void Start()
    {
        
    }
}
