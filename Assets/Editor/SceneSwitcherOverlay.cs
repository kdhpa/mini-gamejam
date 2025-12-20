// Assets/Editor/SceneSwitcherOverlay.cs
using UnityEditor.Overlays;
public class SceneSwitcherOverlay : ToolbarOverlay
{
    // ToolbarOverlay는 "툴바 요소 ID 목록"을 base 생성자에 넘기는 방식
    public SceneSwitcherOverlay() : base(SceneToolbarDropdown.ID) { }
}
