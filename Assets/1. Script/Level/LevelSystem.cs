using UnityEditor;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoSingleton<LevelSystem>
{
    public string url = string.Empty;
    public LevelContainer[] containers;

    private LevelContainer _curContainer;
    public LevelContainer CurContainer => _curContainer;

    public int curIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        containers = Resources.LoadAll<LevelContainer>(url);
        _curContainer = containers[0];
    }

    public void StartLevel( int index )
    {
        curIndex = index;
        LevelContainer container = containers[index];
        _curContainer = container;
        SceneManager.LoadScene("GameScene");
    }

    public void NextLevel()
    {
        if (!IsNextable()) return;

        curIndex++;
        _curContainer = containers[curIndex];
        SceneManager.LoadScene("GameScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public bool IsNextable()
    {
        return containers.Length > curIndex + 1;
    }
}
