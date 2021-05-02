using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    public Rigidbody body;

    [Header("Camera Settings")]
    public Camera cam;
    public float lookHorizontalSensitivity = 1f;
    public float lookVerticalSensitivity = 1f;
    public float minimumPitch = -89.9f;
    public float maximumPitch = 89.9f;

    [Header("Jump Settings")]
    public float jumpForce = 300f;
    public float jumpInputBufferTime = 0.25f;

    [Header("Movement Settings")]
    [Range(0.1f, 15f)]
    public float forwardSpeed = 5f;
    [Range(0.1f, 15f)]
    public float backSpeed = 3f;

    private Vector2 _inputVector = Vector2.zero;
    private bool _jumpInput = false;
    private float _jumpTimeSinceInput = 0.0f;
    private bool _grounded = false;

    private float _lookYaw;
    private float _lookPitch;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _lookYaw = transform.eulerAngles.y;
        _lookPitch = cam.transform.eulerAngles.x;
    }

    void Update()
    {
        Look();
        MovementInput();
    }

    void FixedUpdate()
    {
        MoveLogic();
        JumpLogic();

        UpdateGroundedState();
    }

    void ApplyDirectionalSpeedMultipliers(ref Vector3 vector)
    {
        if (_inputVector.y < 0.0f)
        {
            vector = vector.normalized * backSpeed;
        }
        else
        {
            vector = vector.normalized * forwardSpeed;
        }
    }

    Vector3 GetMovementVector()
    {
        return transform.TransformVector(new Vector3(_inputVector.x, 0.0f, _inputVector.y));
    }

    void Move(Vector3 displacement)
    {
        transform.position += displacement;
    }

    void Look()
    {
        var mouseDeltaYaw = Input.GetAxisRaw("Mouse X");
        var mouseDeltaPitch = Input.GetAxisRaw("Mouse Y");

        _lookYaw += mouseDeltaYaw;
        _lookYaw = WrapAroundAngleDegrees(_lookYaw);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, _lookYaw, transform.eulerAngles.z);

        _lookPitch -= mouseDeltaPitch;
        _lookPitch = ClampPitch(_lookPitch);
        cam.transform.rotation = Quaternion.Euler(_lookPitch, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
    }

    void MovementInput()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_jumpTimeSinceInput >= jumpInputBufferTime)
        {
            _jumpInput = Input.GetButtonDown("Jump");
            if (_jumpInput)
            {
                _jumpTimeSinceInput = 0.0f;
            }
        }
        else
        {
            _jumpTimeSinceInput += Time.deltaTime;
            _jumpTimeSinceInput = 0f;
        }
    }

    void UpdateGroundedState()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo)
        && body.velocity.y < 0.01f
        && hitInfo.distance <= 2.0f / 2.0f)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }

    void MoveLogic()
    {
        ApplyDirectionalSpeedMultipliers(ref moveVector);
        var moveVector = transform.TransformVector(new Vector3(_inputVector.x, 0f, _inputVector.y));
        Move(moveVector * Time.fixedDeltaTime);
    }

    void JumpLogic()
    {
        if (_jumpInput && _grounded)
        {
            _jumpInput = false;
            _jumpTimeSinceInput = jumpInputBufferTime;

            Vector3 keepVelocities = Vector3.right + Vector3.forward;
            body.velocity = Vector3.Scale(body.velocity, keepVelocities);
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    float WrapAroundAngleDegrees(float angle)
    {
        return (angle + 360f) % 360;
    }

    float ClampPitch(float pitch)
    {
        return Mathf.Clamp(pitch, minimumPitch, maximumPitch);
    }
}
