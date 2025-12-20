using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoSingleton<LevelSystem>
{
    public string url = string.Empty;
    public LevelContainer[] containers;

    private LevelContainer _curContainer;
    public LevelContainer CurContainer => _curContainer;

    protected override void Awake()
    {
        base.Awake();
        containers = Resources.LoadAll<LevelContainer>(url);
        _curContainer = containers[0];
    }

    public void StartLevel( int index )
    {
        LevelContainer container = containers[index];
        _curContainer = container;
        SceneManager.LoadScene("GameScene");
    }
}
