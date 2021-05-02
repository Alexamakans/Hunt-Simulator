using UnityEngine;

public class DestroyAfterParticlesEnd : MonoBehaviour
{
    public ParticleSystem particles;

    void Reset()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        if (particles.main.loop)
        {
            Debug.LogWarning("Cannot destroy particle system that loops as it never ends.", this);
            return;
        }

        var lifetimeMode = particles.main.startLifetime.mode;
        if (lifetimeMode != ParticleSystemCurveMode.Constant
        && lifetimeMode != ParticleSystemCurveMode.TwoConstants)
        {
            Debug.LogWarning($"Lifetime mode '{lifetimeMode}' is not supported. Use for example '{nameof(DestroyAfterSeconds)}' component instead.", this);
            return;
        }

        var lifetime = particles.main.startLifetime.constantMax;
        var duration = particles.main.duration;

        Destroy(gameObject, lifetime + duration);
    }
}