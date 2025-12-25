using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField]
    private GameObject firstPanel;
    [SerializeField]
    private GameObject selectLevelPanel;

    public void StartSelectLevel()
    {
        firstPanel.SetActive(false);
        selectLevelPanel.SetActive(true);
    }
}
