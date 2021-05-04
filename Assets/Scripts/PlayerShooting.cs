using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform fireFrom;
    public float fireForce = 1;

    public PlayerInventory playerInventory;

    void Reset()
    {
        playerInventory = GetComponent<PlayerInventory>();
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

        if (!playerInventory.ammunition)
        {
            return;
        }

        var ammoBody = playerInventory.ammunition.body;
        if (!ammoBody)
        {
            Debug.LogWarning("Projectile does not have a Rigidbody.");
            return;
        }
        ammoBody.transform.SetPositionAndRotation(fireFrom.position, fireFrom.rotation);
        ammoBody.AddForce(fireFrom.forward * fireForce);
        playerInventory.OnShoot();
    }
}
