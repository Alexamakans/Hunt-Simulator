using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FpsCamera playerCamera;

    private Vector3 _cameraOffset;

    void Start()
    {
        _cameraOffset = playerCamera.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 angles = transform.eulerAngles;
        angles.y = playerCamera.transform.eulerAngles.y;
        transform.eulerAngles = angles;

        playerCamera.transform.position = transform.position + _cameraOffset;
    }
}
