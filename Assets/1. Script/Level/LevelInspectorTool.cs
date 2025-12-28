using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
public class LevelInspectorTool
{
    Transform target;          // 감시할 대상
    Vector3 lastPos;           // 비교용
    Transform stableObject;    // 갱신할 대상

    LevelInspectorTool()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    public void SetTargets(Transform watch, Transform stable)
    {
        target = watch;
        stableObject = stable;
        lastPos = watch.position;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (target == null || stableObject == null)
            return;

        if (target.position != lastPos)
        {
            OnObjectMoved();

            lastPos = target.position;
        }
    }

    void OnObjectMoved()
    {
        // stableObject 업데이트 로직
        stableObject.position = target.position;
        stableObject.rotation = target.rotation;
    }
}
#endif