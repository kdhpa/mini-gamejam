using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using System.IO;

public class SceneToolbarDropdown : EditorToolbarDropdown
{
    public const string ID = "SceneSwitcher/Dropdown";

    public SceneToolbarDropdown()
    {
        text = "Scene";
        tooltip = "Quick Scene Switcher";

        clicked += ShowMenu;
    }

    void ShowMenu()
    {
        var menu = new GenericMenu();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;

            string sceneName = Path.GetFileNameWithoutExtension(scene.path);

            menu.AddItem(new GUIContent(sceneName), false, () =>
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    EditorSceneManager.OpenScene(scene.path);
            });
        }

        menu.ShowAsContext();
    }
}
