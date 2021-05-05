using UnityEngine;
using System.Collections.Generic;

public class LayerHelper
{
    public static int[] GetAllLayers()
    {
        var layers = new List<int>();
        for (var n = 0; n < 32; n++)
        {
            if (LayerMask.LayerToName(n).Length > 0)
            {
                layers.Add(n);
            }
        }

        return layers.ToArray();
    }
}