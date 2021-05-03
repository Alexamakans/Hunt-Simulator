using UnityEngine;

[RequireComponent(typeof(Health))]
public class TakeDamageFromPlayerProjectile : MonoBehaviour
{
    public Health health;

    void Reset()
    {
        health = GetComponent<Health>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILE))
        {
            health.Damage(Mathf.CeilToInt(collision.relativeVelocity.magnitude));
        }
    }
}
