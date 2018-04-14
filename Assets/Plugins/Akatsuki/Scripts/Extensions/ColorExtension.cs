using UnityEngine;

public static partial class ExtensionMethods
{
    public static string ToHex(this Color color)
    {
        return ((int)(color.r * 255)).ToString("X2") +
               ((int)(color.g * 255)).ToString("X2") +
               ((int)(color.b * 255)).ToString("X2");
    }
}
