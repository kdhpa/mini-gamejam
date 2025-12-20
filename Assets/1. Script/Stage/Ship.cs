using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : CameraAttachObject
{
    public int maxGas;
    public float gas;
    public int gasSpeed = 3;

    public float speed = 0f;
    public float verViewSpeed = 1.0f;
    public float horiViewSpeed = 1.5f;

    private Rigidbody rigid;
    private PlayerInput input;

    private InputAction moveAction;
    private InputAction viewAction;

    private Camera fowardCam;

    private Vector3 moveVec;
    private Vector3 rotateVec;

    public Vector3 velocity = Vector3.zero;
    public Vector3 angularVelocity;

    private MeshRenderer[] meshRenders;
    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        meshRenders = GetComponentsInChildren<MeshRenderer>();
        for ( int i = 0; i<meshRenders.Length; i++)
        {
            GameObject game_object = meshRenders[i].gameObject;
            MeshCollider collider = game_object.GetComponent<MeshCollider>();
            collider.enabled = false;
        }

        InputSetting();
    }

    public void Setting( ShipObject ship_object )
    {
        maxGas = ship_object.maxGas;
        gas = maxGas;
    }

    private void Start()
    {
        fowardCam = cameraDic["FowardCamera"].CAMERA;
    }

    private void InputSetting()
    {
        moveAction = input.actions.FindAction("Move");
        viewAction = input.actions.FindAction("Look");
    }

    private void Update()
    {
        moveVec = moveAction.ReadValue<Vector3>();
        rotateVec = viewAction.ReadValue<Vector2>();

        if (moveVec != Vector3.zero)
        {
            Gas();
        }
    }
    private void FixedUpdate()
    {
        Rolling();
        Move();
    }

    private void Move()
    {
        Vector3 input_move_vec = new Vector3(moveVec.z, moveVec.y, moveVec.x);

        Vector3 foward_vec = fowardCam.transform.forward;
        Vector3 foward_direction = foward_vec.normalized;

        Vector3 right_vec = fowardCam.transform.right;
        Vector3 right_direction = right_vec.normalized;

        Vector3 up_vec = fowardCam.transform.up;
        Vector3 up_direction = up_vec.normalized;

        Vector3 dir = (foward_direction * input_move_vec.z) + (right_direction * input_move_vec.x) + (up_direction * input_move_vec.y);
        Vector3 move_velocity = dir.normalized * (speed * Time.fixedDeltaTime);

        velocity += move_velocity;

        rigid.linearVelocity = velocity;
    }
    private void Rolling()
    {
        Vector3 input = new Vector3(
            rotateVec.y * horiViewSpeed,
            rotateVec.x * verViewSpeed,
            0f
        );

        angularVelocity += input * Time.fixedDeltaTime;

        angularVelocity.x = Mathf.Clamp(angularVelocity.x, -30, 30);
        angularVelocity.y = Mathf.Clamp(angularVelocity.y, -30, 30);

        angularVelocity *= 0.98f;

        // // 회전 적용 (roll 축 제외)
        Quaternion q =
            Quaternion.AngleAxis(angularVelocity.x * Time.fixedDeltaTime, transform.right) *
            Quaternion.AngleAxis(angularVelocity.y * Time.fixedDeltaTime, transform.up);


        Quaternion newRot = q * transform.rotation;
        Vector3 forward = newRot * Vector3.forward;
        newRot = Quaternion.LookRotation(forward, Vector3.up);

        transform.rotation = newRot;
    }
    private void Gas()
    {
        gas -= gasSpeed * Time.deltaTime;
    }
    private void Destory()
    {
        for ( int i = 0; i<meshRenders.Length; i++)
        {
            GameObject game_object = meshRenders[i].gameObject;
            Rigidbody rigid = game_object.GetComponent<Rigidbody>();
            MeshCollider collider = game_object.GetComponent<MeshCollider>();
            collider.enabled = true;
            rigid.isKinematic = false;

            Vector3 dir = new Vector3(Random.Range(-1, 1), Random.Range(-1,1), Random.Range(-1, 1));
            rigid.AddForce(dir.normalized * 50);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        Destory();
    }
}
