using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float seconds = 5;

    void Start()
    {
        Destroy(gameObject, seconds);
    }
}