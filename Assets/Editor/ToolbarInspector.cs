using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

[InitializeOnLoad]
public static class ToolbarInspector
{
    static Type toolbarType;
    static ScriptableObject lastToolbar;

    static ToolbarInspector()
    {
        toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        if (toolbarType == null)
        {
            Debug.LogError("[ToolbarInspector] UnityEditor.Toolbar type NOT FOUND");
            return;
        }

        // Debug.Log($"[ToolbarInspector] Toolbar type found: {toolbarType.FullName}");

        EditorApplication.update += Update;
    }

    static void DumpToolbarFields(ScriptableObject toolbar)
    {
        var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        var fields = toolbar.GetType().GetFields(flags);

        Debug.Log("==== Toolbar Fields ====");
        foreach (var f in fields)
        {
            Debug.Log($"Field: {f.FieldType.Name} {f.Name}");
        }
    }

    static void Update()
    {
        var toolbars = Resources.FindObjectsOfTypeAll(toolbarType);

        if (toolbars.Length == 0)
        {
            if (lastToolbar != null)
            {
                //Debug.Log("[ToolbarInspector] Toolbar DESTROYED");
                lastToolbar = null;
            }
            return;
        }

        var current = toolbars[0] as ScriptableObject;

        if (current != lastToolbar)
        {
            //Debug.Log($"[ToolbarInspector] Toolbar instance FOUND / CHANGED: {current.GetInstanceID()}");
            lastToolbar = current;
            //DumpToolbarFields(current);
        }
    }
}
