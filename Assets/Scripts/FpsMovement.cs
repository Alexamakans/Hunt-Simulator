using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    [Range(0.1f, 15.0f)]
    public float forwardSpeed = 1.0f;
    [Range(0.1f, 15.0f)]
    public float backSpeed = 0.6f;

    private Vector2 _inputVector = Vector2.zero;

    void Update()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = GetMovementVector();
        ApplyDirectionalSpeedMultipliers(ref moveVector);

        Move(moveVector * Time.fixedDeltaTime);
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
