using UnityEngine;

public class ResetWhenBelowY : MonoBehaviour
{
    public float minY = -10;
    public Vector3 resetTo = Vector3.up * 10;
    public Vector3 resetRandomRegionSize = new Vector3(10, 0, 10);
    public Rigidbody body;
    public ResetPlayerVisual visual;

    void Reset()
    {
        body = GetComponentInChildren<Rigidbody>();
        visual = GetComponentInChildren<ResetPlayerVisual>();
    }

    void OnEnable()
    {
        if (!body)
        {
            Debug.LogWarning($"Cannot reset when below Y without '{nameof(body)}' set to a Rigidbody.", this);
            enabled = false;
        }

        if (!visual)
        {
            Debug.LogWarning($"Cannot reset when below Y without '{nameof(visual)}' set to a {nameof(ResetPlayerVisual)}.", this);
            enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        var pos = transform.position;
        pos.y = 0;
        Gizmos.DrawCube(pos + Vector3.up * minY, new Vector3(100, 0, 100));
        Gizmos.DrawCube(resetTo, resetRandomRegionSize);
    }

    void Update()
    {
        if (visual.isPlaying)
        {
            return;
        }

        if (transform.position.y < minY)
        {
            visual.StartFading(this, nameof(ResetPosition));
        }
    }

    public void ResetPosition()
    {
        if (!body)
        {
            Debug.LogWarning($"Cannot reset when below Y without '{nameof(body)}' set to a Rigidbody.", this);
            return;
        }

        var halfSize = resetRandomRegionSize * 0.5f;
        body.position = resetTo + new Vector3(
            Random.Range(-halfSize.x, halfSize.x),
            Random.Range(-halfSize.y, halfSize.y),
            Random.Range(-halfSize.z, halfSize.z)
        );
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }
}
