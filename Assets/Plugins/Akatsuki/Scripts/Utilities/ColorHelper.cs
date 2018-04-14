using System;
using System.Globalization;
using UnityEngine;

namespace Akatsuki
{
    public class ColorHelper
    {
        public static Color HexToColor(string hex)
        {
            var r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
    }
}
