using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SingleLayerAttribute : PropertyAttribute
{
    public SingleLayerAttribute() { }
}
