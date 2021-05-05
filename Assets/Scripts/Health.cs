using UnityEngine;

[RequireComponent(typeof(IDeathListener))]
public class Health : MonoBehaviour
{
    public int health;
    public bool isDead => health <= 0;

    public void Damage(int amount)
    {
        health -= amount;
        if (isDead)
        {
            var listeners = GetComponentsInChildren<IDeathListener>();
            foreach (var listener in listeners)
            {
                listener.OnDeath();
            }
        }
    }
}
