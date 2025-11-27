using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject scroll_object;
    private ScrollView scroll_view;

    private void Awake()
    {
        scroll_view = scroll_object.GetComponent<ScrollView>();
    }

    private void Start()
    {
        
    }
}
