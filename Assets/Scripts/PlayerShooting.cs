using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform fireFrom;
    public float fireForce = 1;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        if (!projectilePrefab)
        {
            Debug.LogWarning($"Cannot fire projectile: {nameof(projectilePrefab)} is unset.", this);
            return;
        }

        if (!fireFrom)
        {
            Debug.LogWarning($"Cannot fire projectile: {nameof(fireFrom)} is unset.", this);
            return;
        }

        var clone = Instantiate(projectilePrefab, fireFrom.position, fireFrom.rotation);
        var body = clone.GetComponentInChildren<Rigidbody>();

        if (!body)
        {
            Debug.LogWarning("Projectile prefab does not have a RigidBody.", this);
        }
        else
        {
            body.AddForce(fireFrom.forward * fireForce);
        }
    }
}
