using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    public float lookYawSensitivity = 1.0f;
    [Range(0.1f, 10.0f)]
    public float lookPitchSensitivity = 1.0f;

    [Range(-89.9f, -0.1f)]
    public float minimumPitch = -89.9f;
    [Range(0.1f, 89.9f)]
    public float maximumPitch = 89.9f;

    private float _yaw = 0.0f;
    private float _pitch = 0.0f;
    private float _lookSensitivityScale = 0.25f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float deltaYaw = Input.GetAxisRaw("Mouse X") * lookYawSensitivity * _lookSensitivityScale;
        float deltaPitch = -Input.GetAxisRaw("Mouse Y") * lookPitchSensitivity * _lookSensitivityScale;

        _yaw += deltaYaw;
        _yaw = WrapAroundAngle(_yaw);

        _pitch += deltaPitch;
        _pitch = ClampPitch(_pitch);

        transform.forward = Quaternion.Euler(_pitch, _yaw, 0.0f) * Vector3.forward;
    }

    private float WrapAroundAngle(float angle)
    {
        if (angle > Mathf.PI)
        {
            return angle - Mathf.PI * 2.0f;
        }
        else if (angle < -Mathf.PI)
        {
            return angle + Mathf.PI * 2.0f;
        }

        return angle;
    }

    private float ClampPitch(float pitch)
    {
        if (pitch < minimumPitch)
        {
            pitch = minimumPitch;
        }

        if (pitch > maximumPitch)
        {
            pitch = maximumPitch;
        }

        return pitch;
    }
}