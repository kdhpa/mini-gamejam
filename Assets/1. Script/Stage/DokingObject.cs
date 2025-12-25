using System;
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

    private bool isDockable = false;
    private Transform playerTransfom;
    private bool IsDockingCompleate(Transform trans)
    {
        float percentage = GetDockingPercentage(trans);
        return percentage >= successThreshold;
    }

    public float GetDockingPercentage(Transform playerTransform)
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float distancePercent = 100 - Mathf.Clamp01(distance / maxDockingRange) * 100;
        
        return distancePercent;
    }

    private void Update()
    {
        if(isDockable)
        {
            if (IsDockingCompleate(playerTransfom))
            {
                EventManager.Instance.Trigger("Clear", this);
                return;
            }

            EventManager.Instance.Trigger("DockPercent", this, new DockPercentEventArgs{
                dockPercent = GetDockingPercentage(playerTransfom)
                });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Dock" )
        {
            EventManager.Instance.Trigger("Dock", this, new EventArgs() );

            playerTransfom = other.transform;
            isDockable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Dock" )
        {
            EventManager.Instance.Trigger("Dock", this );
            isDockable = false;
        }
    }
}

public class DockPercentEventArgs : EventArgs
{
    public float dockPercent;
}