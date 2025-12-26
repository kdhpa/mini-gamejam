using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GasChangeEventArgs : EventArgs
{
    public float CurrentGas { get; set; }
    public float MaxGas { get; set; }
    public float Percentage => MaxGas > 0 ? CurrentGas / MaxGas : 0;
}

public class AxisChangeEventArgs : EventArgs
{
    public Vector3 axis;
}

public class Ship : CameraAttachObject
{
    [SerializeField]
    private FowardCamera fowardCamera;

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
    private InputAction dockAction;
    private InputAction brakeAction;

    private Camera fowardCam;

    private Vector3 moveVec;
    private Vector3 rotateVec;

    public Vector3 velocity = Vector3.zero;
    public Vector3 angularVelocity;

    private MeshRenderer[] meshRenders;

    private bool isDestory = false;
    private bool isClear = false;
    private bool isDock = false;
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
        gasSpeed = ship_object.gasSpeed;
    }

    private void Start()
    {
        fowardCam = cameraDic["FowardCamera"].CAMERA;
        EventManager.Instance.AddEventListner("Fail", DestoryEvent);
        EventManager.Instance.AddEventListner("Clear", ClearEvent);
    }

    private void InputSetting()
    {
        moveAction = input.actions.FindAction("Move");
        viewAction = input.actions.FindAction("Look");
        dockAction = input.actions.FindAction("Dock");

        dockAction.performed += Dock;
    }

    private void Update()
    {
        if(isDestory || isClear) return;
        moveVec = moveAction.ReadValue<Vector3>();
        rotateVec = viewAction.ReadValue<Vector2>();

        if (moveVec != Vector3.zero)
        {
            Jet();
        }
    }
    private void FixedUpdate()
    {
        if(isDestory || isClear) return;

        CheckDock();
        Rolling();
        Move();
    }

    private void CheckDock()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, 10, LayerMask.GetMask("Dock")))
        {
            isDock = true;
        }
        else
        {
            isDock = false;
        }
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

        EventManager.Instance.Trigger("AxisChange", this, new AxisChangeEventArgs(){ axis = velocity });
        EventManager.Instance.Trigger("InputChange", this, new AxisChangeEventArgs(){ axis = input_move_vec });

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
        EventManager.Instance.Trigger("RotateChange", this, new AxisChangeEventArgs(){ axis = angularVelocity });

        // // 회전 적용 (roll 축 제외)
        Quaternion q =
            Quaternion.AngleAxis(angularVelocity.x * Time.fixedDeltaTime, transform.right) *
            Quaternion.AngleAxis(angularVelocity.y * Time.fixedDeltaTime, transform.up);


        Quaternion newRot = q * transform.rotation;
        Vector3 forward = newRot * Vector3.forward;
        newRot = Quaternion.LookRotation(forward, Vector3.up);

        transform.rotation = newRot;
    }

    private void Jet()
    {
        gas = gas - ( gasSpeed * Time.deltaTime );
        GasChangeEventArgs args = new GasChangeEventArgs
        {
            CurrentGas = gas,
            MaxGas = maxGas
        };

        if (gas <= 0)
        {
            EventManager.Instance.Trigger("Fail", this);
        }
        else
        {
            EventManager.Instance.Trigger("GasChange", this, args);
        }
    }

    private void Dock(InputAction.CallbackContext context)
    {
        if (!isDock) return;

        velocity += transform.up * -30;
    }

    private void Destory()
    {
        isDestory = true;
        for ( int i = 0; i<meshRenders.Length; i++)
        {
            GameObject game_object = meshRenders[i].gameObject;
            Rigidbody rigid = game_object.GetComponent<Rigidbody>();
            MeshCollider collider = game_object.GetComponent<MeshCollider>();
            collider.enabled = true;
            rigid.isKinematic = false;

            Vector3 dir = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-1, 1));
            rigid.AddForce(dir.normalized * 50);
        }
    }
    private void DestoryEvent(object sender, EventArgs args)
    {
        if (isDestory) return;
        Destory();
        rigid.linearVelocity = Vector3.zero;
        rigid.isKinematic = true;
    }

    private void ClearEvent(object sender, EventArgs args)
    {
        if (isClear) return;
        isClear = true;
        rigid.linearVelocity = Vector3.zero;
        rigid.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dock") return;
        EventManager.Instance.Trigger("Fail", this);
    }
}
