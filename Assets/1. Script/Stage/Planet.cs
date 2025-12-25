using UnityEngine;

public class Planet : MonoBehaviour
{
    public float size;
    
    public float gravityRadius = 50f; // 중력이 작용하는 범위
    public float gravitationalConstant = 100f; // 중력 강도

    public Vector3 rotationDir;
    public float rotationSpeed;

    public Vector3 revDir;
    public float revSpeed = 5f;

    private MeshRenderer mesh_renderer;
    private void Start()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        transform.eulerAngles += Vector3.Normalize(rotationDir) * rotationSpeed * Time.deltaTime;
    }

    public void SettingPlanet(PlaneObject pl_ojbect)
    {
        revSpeed = pl_ojbect.revSpeed;
        rotationSpeed = pl_ojbect.rotSpeed;
        rotationDir = pl_ojbect.rotDir;
        revDir = pl_ojbect.revDir; 
    }
    
    private void FixedUpdate()
    {
        // 범위 내의 모든 Ship 찾기
        Collider[] colliders = Physics.OverlapSphere(transform.position, gravityRadius);

        foreach (Collider col in colliders)
        {
            Ship ship = col.GetComponent<Ship>();
            if (ship != null)
            {
                ApplyGravity(ship);
            }
        }
    }
    private void ApplyGravity(Ship ship)
    {
        Vector3 toCenter = transform.position - ship.transform.position;
        float distance = toCenter.magnitude;
    
        // 행성 쪽으로 당기는 힘
        Vector3 gravityForce = toCenter.normalized * (gravitationalConstant / distance);
    
        // 현재 속도와 중력 방향의 각도 계산
        float angle = Vector3.Angle(ship.velocity, toCenter);
    
        // 스윙바이 효과: 각도가 적당하면 속도 증폭
        if (angle > 45f && angle < 135f)
        {
            float swingByBoost = 2f; // 10% 부스트
            ship.velocity += revDir * swingByBoost;
        }
    
        ship.velocity += gravityForce * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, gravityRadius);
    }
}
