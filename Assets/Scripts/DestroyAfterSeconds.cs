using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds = 5;

    void Start()
    {
        Destroy(gameObject, seconds);
    }
}