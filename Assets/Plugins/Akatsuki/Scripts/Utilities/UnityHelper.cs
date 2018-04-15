using System;
using UnityEngine;

namespace Akatsuki
{
    public class UnityHelper
    {
        public static Texture2D CreateTexture(Color color)
        {
            var array = new Color32[3600];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = color;
            }

            var texture2D = new Texture2D(60, 60);
            texture2D.SetPixels32(array);
            texture2D.wrapMode = TextureWrapMode.Clamp;
            texture2D.Apply();
            return texture2D;
        }
    }
}
