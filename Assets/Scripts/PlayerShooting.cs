using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform fireFrom;
    public float fireForce = 1;

    public PlayerInventory pickupAmmunition;

    void Reset()
    {
        pickupAmmunition = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        if (!fireFrom)
        {
            Debug.LogWarning($"Cannot fire projectile: {nameof(fireFrom)} is unset.", this);
            return;
        }

        if (!pickupAmmunition.ammunition)
        {
            return;
        }

        var ammoBody = pickupAmmunition.ammunition.GetComponent<Rigidbody>();
        if (!ammoBody)
        {
            Debug.LogWarning("Projectile does not have a Rigidbody.", this);
            return;
        }

        ammoBody.transform.SetPositionAndRotation(fireFrom.position, fireFrom.rotation);
        ammoBody.AddForce(fireFrom.forward * fireForce);
        pickupAmmunition.OnShoot();
    }
}
