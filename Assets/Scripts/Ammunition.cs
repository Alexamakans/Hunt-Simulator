using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammunition : MonoBehaviour
{
    public Rigidbody body;
    public GameObject impactParticles;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public bool canBePickedUp;
    [SingleLayer]
    public int ignorePlayerLayer = 7;

    private Vector3 _originalScale;
    private int _originalLayer;

    void Reset()
    {
        body = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        if (canBePickedUp)
        {
            _originalScale = transform.localScale;
            _originalLayer = gameObject.layer;
            
            gameObject.layer = ignorePlayerLayer;
            
            body.detectCollisions = false;
            body.isKinematic = true;

            var size = GetComponent<MeshFilter>().mesh.bounds.size;
            transform.localScale = new Vector3(inHandSize.x / size.x, inHandSize.y / size.y, inHandSize.z / size.z);
        }
    }

    public void OnShoot()
    {
        tag = Tags.PLAYER_PROJECTILE;
        canBePickedUp = false;

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
    }
}
