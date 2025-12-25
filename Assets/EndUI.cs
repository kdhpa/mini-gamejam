using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    [SerializeField]
    protected GameObject panel;

    [SerializeField]
    protected string tagName = string.Empty;

    protected virtual void Start()
    {
        EventManager.Instance.AddEventListner(tagName, End);
    }

    protected virtual void End(object sender, EventArgs eventArgs)
    {
        StartCoroutine(ClearActive());
    }

    protected virtual IEnumerator ClearActive()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(true);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Lobby");
    }

}
