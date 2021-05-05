using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public LayerMask pickUpLayerMask = 1;
    public float pickUpRange = 2f;
    public Transform hand;
    public Vector3 inHandSize = new Vector3(0.2f, 0.2f, 0.2f);
    public Ammunition ammunition;

    void Update()
    {
        if (ammunition)
        {
            ammunition.transform.position = hand.position;
        }
        else if (Input.GetButtonDown("Fire2")
                && Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out var hitInfo,
                    pickUpRange,
                    pickUpLayerMask))
        {
            var collidedAmmo = hitInfo.collider.GetComponent<Ammunition>();
            if (collidedAmmo && collidedAmmo.canBePickedUp)
            {
                ammunition = collidedAmmo;
                ammunition.inHandSize = inHandSize;
                ammunition.PickUp();
            }
        }
    }

    public void OnShoot()
    {
        ammunition.OnShoot();
        ammunition = null;
    }
}
