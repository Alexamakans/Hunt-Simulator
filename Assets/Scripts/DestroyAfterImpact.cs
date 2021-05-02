using UnityEngine;

public class DestroyAfterImpact : MonoBehaviour
{
    public GameObject impactParticles;
    public bool impactOnAveragePoint;

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);

        if (impactParticles)
        {
            if (impactOnAveragePoint)
            {
                var average = col.GetAverageContact();
                SpawnImpactParticlesOnPoint(average.normal, average.point);
            }
            else
            {
                SpawnImpactParticlesOnAllContacts(col);
            }
        }
    }

    void SpawnImpactParticlesOnAllContacts(Collision col)
    {
        for (int i = 0; i < col.contactCount; i++)
        {
            var contact = col.GetContact(i);
            SpawnImpactParticlesOnPoint(contact.normal, contact.point);
        }
    }

    void SpawnImpactParticlesOnPoint(Vector3 normal, Vector3 point)
    {
        var rot = Quaternion.LookRotation(normal);
        Instantiate(impactParticles, point, rot);
    }
}