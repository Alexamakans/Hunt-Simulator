using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public LayerMask ignoreLayers = 1 << 4;
    public float pickUpRange = 2f;
    public Transform hand;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public GameObject projectileHitParticles;
    public Ammunition ammunition;

    void Update()
    {
        if (ammunition)
        {
            ammunition.transform.position = hand.position;
            ammunition.body.velocity = Vector3.zero;
        }
        else if (Input.GetButtonDown("Fire2")
                && Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out var hitInfo,
                    pickUpRange,
                    ~ignoreLayers))
        {
            var collider = hitInfo.collider;
            ammunition = collider.GetComponent<Ammunition>();
            if (ammunition)
            {
                if (ammunition.canBePickedUp)
                {
                    ammunition.inHandSize = inHandSize;
                    ammunition.PickUp();
                }
                else
                {
                    ammunition = null;
                }
            }
        }
    }

    public void OnShoot()
    {
        ammunition.OnShoot(projectileHitParticles);
        ammunition = null;
    }
}
