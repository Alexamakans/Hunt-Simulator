using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private float _health = 100.0f;

    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILE))
        {
            ContactPoint cp = collision.GetContact(0);

            Vector3 velMe = _rigidbody.GetPointVelocity(cp.point);
            Vector3? velOther = collision.rigidbody?.GetPointVelocity(cp.point);

            float force = 1.0f;

            if (velOther.HasValue)
            {
                Vector3 relativeVel = (velMe - velOther.Value);
                force = Vector3.Dot(cp.normal, relativeVel);
            }

            Debug.Log("Impact Force: " + force);
        }
    }
}
