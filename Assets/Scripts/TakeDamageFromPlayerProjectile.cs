using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class TakeDamageFromPlayerProjectile : MonoBehaviour
{
    public float health = 25.0f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILE))
        {
            float force = collision.relativeVelocity.magnitude;
            health -= force;

            if (health <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
