using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public float speed = 0f;
    public float ver_view_speed = 1.0f;
    public float hori_view_speed = 1.5f;

    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;
    private PlayerInput input;

    private InputAction move_action;
    private InputAction view_action;

    private Vector3 move_vec;
    private Vector3 rotate_vec;

    public Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Camera foward_cam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

        Vector3 foward_vec = foward_cam.transform.forward;
        Vector3 foward_direction = foward_vec.normalized;

        Vector3 right_vec = foward_cam.transform.right;
        Vector3 right_direction = right_vec.normalized;

        Vector3 up_vec = foward_cam.transform.up;
        Vector3 up_direction = up_vec.normalized;

        Vector3 dir = (foward_direction * input_move_vec.z) + (right_direction * input_move_vec.x) + (up_direction * input_move_vec.y);
        Vector3 move_velocity = dir.normalized * (speed * Time.fixedDeltaTime);

        velocity += move_velocity;

        rigid.linearVelocity = velocity;
    }

    private void Rolling()
    {
        Vector3 input_view_vec = new Vector3(rotate_vec.y, rotate_vec.x, 0);
        Quaternion deltaRotation = Quaternion.Euler(input_view_vec.x * ver_view_speed * Time.fixedDeltaTime, input_view_vec.y * hori_view_speed * Time.fixedDeltaTime, 0);
        rigid.MoveRotation(transform.localRotation * deltaRotation);
    }
}
