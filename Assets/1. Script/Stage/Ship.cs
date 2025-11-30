using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : CameraAttachObject
{
    public float speed = 0f;
    public float ver_view_speed = 1.0f;
    public float hori_view_speed = 1.5f;

    private Rigidbody rigid;
    private PlayerInput input;

    private InputAction move_action;
    private InputAction view_action;

    private Vector3 move_vec;
    private Vector3 rotate_vec;

    public Vector3 velocity = Vector3.zero;
    public Vector3 angularVelocity;

    private void Awake()
    {
        camera_objects = GetComponentsInChildren<CameraObject>();

        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        InputSetting();
    }

    private void InputSetting()
    {
        move_action = input.actions.FindAction("Move");
        view_action = input.actions.FindAction("Look");
    }

    private void Update()
    {
        move_vec = move_action.ReadValue<Vector3>();
        rotate_vec = view_action.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Rolling();
        Move();
    }

    private void Move()
    {
        Vector3 input_move_vec = new Vector3(move_vec.z, move_vec.y, move_vec.x);

        Vector3 foward_vec = this.transform.forward;
        Vector3 foward_direction = foward_vec.normalized;

        Vector3 right_vec = this.transform.right;
        Vector3 right_direction = right_vec.normalized;

        Vector3 up_vec = this.transform.up;
        Vector3 up_direction = up_vec.normalized;

        Vector3 dir = (foward_direction * input_move_vec.z) + (right_direction * input_move_vec.x) + (up_direction * input_move_vec.y);
        Vector3 move_velocity = dir.normalized * (speed * Time.fixedDeltaTime);

        velocity += move_velocity;

        rigid.linearVelocity = velocity;
    }
    private void Rolling()
    {
        Vector3 input = new Vector3(
            rotate_vec.y * hori_view_speed,
            rotate_vec.x * ver_view_speed,
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
}
