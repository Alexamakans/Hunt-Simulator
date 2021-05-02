using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    public Rigidbody body;

    [Range(0.1f, 15.0f)]
    public float forwardSpeed = 1.0f;
    [Range(0.1f, 15.0f)]
    public float backSpeed = 0.6f;

    public float jumpForce = 10.0f;

    [SerializeField]
    private Vector2 _inputVector = Vector2.zero;
    [SerializeField]
    private bool _jumpInput = false;
    [SerializeField]
    private float _jumpTimeSinceInput = 0.0f;
    [SerializeField]
    private float _jumpInputBufferTime = 0.25f;
    [SerializeField]
    private bool _grounded = false;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_jumpTimeSinceInput >= _jumpInputBufferTime)
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
        }
    }

    private void FixedUpdate()
    {
        // Move
        Vector3 moveVector = GetMovementVector();
        ApplyDirectionalSpeedMultipliers(ref moveVector);

        Move(moveVector * Time.fixedDeltaTime);

        // Jump
        if (_jumpInput && _grounded)
        {
            _jumpInput = false;
            _jumpTimeSinceInput = _jumpInputBufferTime;

            Vector3 keepVelocities = Vector3.right + Vector3.forward;
            body.velocity = Vector3.Scale(body.velocity, keepVelocities);
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            if (body.velocity.y < 0.01f && hitInfo.distance <= 2.0f / 2.0f)
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }
        }
    }

    private void ApplyDirectionalSpeedMultipliers(ref Vector3 vector)
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

    private Vector3 GetMovementVector()
    {
        return transform.TransformVector(new Vector3(_inputVector.x, 0.0f, _inputVector.y));
    }

    private void Move(Vector3 displacement)
    {
        transform.position += displacement;
    }
}
