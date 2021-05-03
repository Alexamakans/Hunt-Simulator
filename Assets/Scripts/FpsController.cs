using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    public Rigidbody body;

    [Header("Camera Settings")]
    public GameObject cam;
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
    public Vector3 velocity { get { return _prevPosition - transform.position; } }

    private Vector3 _prevPosition;

    private Vector2 _inputVector = Vector2.zero;
    private bool isJumpQueued => _jumpTimeSinceInput < jumpInputBufferTime;
    private float _jumpTimeSinceInput;
    private bool _isGrounded = false;

    private float _lookYaw;
    private float _lookPitch;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _lookYaw = transform.eulerAngles.y;
        _lookPitch = cam.transform.eulerAngles.x;
        _jumpTimeSinceInput = jumpInputBufferTime;
        _prevPosition = transform.position;
    }

    void Update()
    {
        Look();
        MovementInput();
    }

    void FixedUpdate()
    {
        Walk();

        if (isJumpQueued && _isGrounded)
        {
            _jumpTimeSinceInput = jumpInputBufferTime;
            Jump();
        }

        UpdateGroundedState();

        _prevPosition = transform.position;
    }

    void Move(Vector3 displacement)
    {
        body.position += displacement;
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

        if (isJumpQueued)
        {
            _jumpTimeSinceInput += Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpTimeSinceInput = 0f;
        }
    }

    void UpdateGroundedState()
    {
        if (body.velocity.y < 0.01f && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    void Walk()
    {
        var moveVector = transform.TransformVector(new Vector3(_inputVector.x, 0f, _inputVector.y));

        var isMovingBackwards = _inputVector.y < 0f;
        moveVector = moveVector.normalized * (isMovingBackwards ? backSpeed : forwardSpeed);

        Move(moveVector * Time.fixedDeltaTime);
    }

    void Jump()
    {
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
