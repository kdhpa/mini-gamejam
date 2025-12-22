using UnityEngine;

public class DokingObject : MonoBehaviour
{
    [Header("도킹 설정")]
    [Tooltip("도킹 성공으로 인정할 거리")]
    public float dockingDistance = 2f;
    
    [Tooltip("도킹 시도 가능한 최대 거리")]
    public float maxDockingRange = 10f;
    
    [Tooltip("도킹 성공 최소 퍼센테이지 (0~1)")]
    public float successThreshold = 0.9f;

    private bool IsDockingCompleate(Collider col)
    {
        float percentage = GetDockingPercentage(col.transform);
        return percentage >= successThreshold;
    }

    public float GetDockingPercentage(Transform playerTransform)
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float distancePercent = 1f - Mathf.Clamp01(distance / maxDockingRange);
        
        return distancePercent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Dock" )
        {
            if (IsDockingCompleate(other))
            {
                EventManager.Instance.Trigger("Clear", this);
            }
            else
            {
                EventManager.Instance.Trigger("Fail", this);
            }
        }
    }
}
