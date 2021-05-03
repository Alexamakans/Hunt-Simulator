using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammunition : MonoBehaviour
{
    public Transform hand;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);

    public Vector3 originalScale;

    private int _originalLayer;
    private Rigidbody _body;

    void Start()
    {
        originalScale = transform.localScale;

        var size = GetComponent<MeshFilter>().mesh.bounds.size;
        transform.localScale = new Vector3(inHandSize.x / size.x, inHandSize.y / size.y, inHandSize.z / size.z);

        _body = GetComponent<Rigidbody>();
        _body.detectCollisions = false;

        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Player");
    }

    void Update()
    {
        if (hand)
        {
            transform.position = hand.position;
            _body.velocity = Vector3.zero;
        }
    }

    public void OnShoot(GameObject impactParticles)
    {
        hand = null;
        tag = Tags.PLAYER_PROJECTILE;

        _body.detectCollisions = true;
        transform.localScale = originalScale;

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
