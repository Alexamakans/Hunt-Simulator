using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public Rigidbody body;
    public GameObject impactParticles;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public bool canBePickedUp;

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
        body.isKinematic = true;

        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Player");
    }

    public void OnShoot()
    {
        body.detectCollisions = true;
        body.isKinematic = false;
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
