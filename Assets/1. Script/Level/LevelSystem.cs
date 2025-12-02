using UnityEditor;
using UnityEngine;

public class LevelSystem : MonoSingleton<LevelSystem>
{
    public string url = string.Empty;
    private LevelContainer[] containers;

    private void Awake()
    {
        containers = Resources.LoadAll<LevelContainer>("Assets/1. Script/Level/Levels");
    }

    public void StartLevel( int index )
    {
        LevelContainer container = containers[index];
        ControlManager.Instance.StartGame(container);
    }
}
