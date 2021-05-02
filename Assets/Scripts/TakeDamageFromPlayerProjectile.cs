using UnityEngine;

public class TakeDamageFromPlayerProjectile : MonoBehaviour
{
    public float health = 25.0f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILE))
        {
            float damage = collision.relativeVelocity.magnitude;
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
