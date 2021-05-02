using UnityEngine;

public class TakeDamageFromPlayerProjectile : MonoBehaviour
{
    public int health = 25;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILE))
        {
            var damage = Mathf.CeilToInt(collision.relativeVelocity.magnitude);
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
