using UnityEngine;

public class PickupAmmunition : MonoBehaviour
{
    public LayerMask ignoreLayers;
    public float pickUpRange;
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
                    ignoreLayers))
        {
            var collider = hitInfo.collider;
            ammunition = collider.GetComponent<Ammunition>();
            if (ammunition)
            {
                ammunition.inHandSize = inHandSize;
            }
        }
    }

    public void OnShoot()
    {
        ammunition.OnShoot(projectileHitParticles);
        ammunition = null;
    }
}
