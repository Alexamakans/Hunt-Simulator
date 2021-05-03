using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(TakeDamageFromPlayerProjectile),
    typeof(Rigidbody)
)]
public class Enemy : MonoBehaviour, IDeathListener
{
    public Material deadMaterial;
    public Ammunition ammunition;

    void Reset()
    {
        ammunition = GetComponent<Ammunition>();
    }

    public void OnDeath()
    {
        GetComponent<MeshRenderer>().material = deadMaterial;

        Destroy(this);
        Destroy(GetComponent<TakeDamageFromPlayerProjectile>());
        Destroy(GetComponent<Health>());

        ammunition.canBePickedUp = true;
    }
}
