using Unity.Collections;
using UnityEngine;

public static class CollisionExtensions
{
    public static AverageContactPoint GetAverageContact(this Collision col)
    {
        using var points = new NativeArray<ContactPoint>(col.contactCount, Allocator.Temp);
        col.GetContacts(points);
        
        var normalSum = Vector3.zero;
        var pointSum = Vector3.zero;
        var div = 1.0f / points.Length;

        foreach (var point in points)
        {
            pointSum += point.point;
            normalSum += point.normal;
        }

        return new AverageContactPoint {
            point = pointSum / points.Length,
            normal = normalSum.normalized,
        };
    }

    public static void GetContacts(this Collision col, NativeArray<ContactPoint> arr)
    {
        for (int i = 0; i < col.contactCount; i++)
        {
            arr[i] = col.GetContact(i);
        }
    }
}

public struct AverageContactPoint
{
    public Vector3 point;
    public Vector3 normal;
}