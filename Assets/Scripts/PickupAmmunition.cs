using UnityEngine;

public class PickupAmmunition : MonoBehaviour
{
    public Transform hand;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public GameObject projectileHitParticles;

    public Ammunition ammunition;

    void Update()
    {
        if (!ammunition
            && Input.GetButtonDown("Fire2")
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 2f, LayerMask.NameToLayer("Player")))
        {
            var collider = hitInfo.collider;
            if (collider.CompareTag(Tags.AMMUNITION)
                && !collider.GetComponent<Ammunition>())
            {
                var go = collider.gameObject;
                ammunition = go.AddComponent<Ammunition>();
                ammunition.hand = hand;
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
