using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public FpsController fpsController;

    public float bobbingVerticalIntensity = 0.025f;
    public float bobbingVerticalFrequency = 16f;

    public float bobbingHorizontalIntensity = 0.015f;
    public float bobbingHorizontalFrequency = 8f;

    public float bobbingSpeed = 9f;

    public float jumpBobIntensity = 0.25f;

    private Vector3 _defaultLocalPosition;
    private float _timer;


    private void Awake()
    {
        _defaultLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        var velocity = fpsController.velocity;
        var planeVelocity = velocity;
        planeVelocity.y = 0f;

        var planeVelocityMagnitude = planeVelocity.magnitude;

        if (planeVelocityMagnitude > 0f || Mathf.Abs(velocity.y) > 0f)
        {
            _timer += Time.deltaTime * planeVelocityMagnitude * bobbingSpeed;
            transform.localPosition = new Vector3(
                _defaultLocalPosition.x + Mathf.Sin(_timer * bobbingHorizontalFrequency) * bobbingHorizontalIntensity,
                _defaultLocalPosition.y + Mathf.Sin(_timer * bobbingVerticalFrequency) * bobbingVerticalIntensity + velocity.y * jumpBobIntensity,
                transform.localPosition.z);
        }
        else
        {
            _timer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _defaultLocalPosition, Time.deltaTime * bobbingSpeed);
        }
    }
}
