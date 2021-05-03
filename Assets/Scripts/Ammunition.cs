using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammunition : MonoBehaviour
{
    public Rigidbody body;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public bool canBePickedUp;
    public LayerMask ignorePlayerLayer = 1 << 7;

    private Vector3 _originalScale;
    private int _originalLayer;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        if (!canBePickedUp)
        {
            _originalScale = transform.localScale;
            _originalLayer = gameObject.layer;
            
            gameObject.layer = ignorePlayerLayer;
            
            body.detectCollisions = false;

            var size = GetComponent<MeshFilter>().mesh.bounds.size;
            transform.localScale = new Vector3(inHandSize.x / size.x, inHandSize.y / size.y, inHandSize.z / size.z);
        }
    }

    public void OnShoot(GameObject impactParticles)
    {
        canBePickedUp = false;

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
