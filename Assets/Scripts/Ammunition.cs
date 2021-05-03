using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public Rigidbody body;
    public Transform hand;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);

    private Vector3 _originalScale;
    private int _originalLayer;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        _originalScale = transform.localScale;

        var size = GetComponent<MeshFilter>().mesh.bounds.size;
        transform.localScale = new Vector3(inHandSize.x / size.x, inHandSize.y / size.y, inHandSize.z / size.z);

        body.detectCollisions = false;

        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Player");
    }

    void Update()
    {
        if (hand)
        {
            transform.position = hand.position;
            body.velocity = Vector3.zero;
        }
    }

    public void OnShoot(GameObject impactParticles)
    {
        hand = null;

        body.detectCollisions = true;
        transform.localScale = _originalScale;

        gameObject.AddComponent<DestroyAfterSeconds>().seconds = 5f;

        var destroyAfterImpact = gameObject.AddComponent<DestroyAfterImpact>();
        destroyAfterImpact.impactOnAveragePoint = true;
        destroyAfterImpact.impactParticles = impactParticles;

        Invoke(nameof(UnignorePlayer), 1.0f);
    }

    void UnignorePlayer()
    {
        gameObject.layer = _originalLayer;
        Destroy(this);
    }
}
