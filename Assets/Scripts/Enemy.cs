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

    public void OnDeath()
    {
        tag = Tags.AMMUNITION;
        GetComponent<MeshRenderer>().material = deadMaterial;

        Destroy(this);
        Destroy(GetComponent<TakeDamageFromPlayerProjectile>());
        Destroy(GetComponent<Health>());
    }
}
