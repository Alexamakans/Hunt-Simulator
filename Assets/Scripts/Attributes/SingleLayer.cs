using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SingleLayer : PropertyAttribute
{
    public SingleLayer() { }
}
