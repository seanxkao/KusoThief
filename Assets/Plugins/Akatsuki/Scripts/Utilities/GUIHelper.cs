using System.Collections.Generic;
using UnityEngine;

namespace Akatsuki
{
    public class GUIHelper
    {
        public static void FixedLabel(string text)
        {
            var width = GUI.skin.label.CalcSize(new GUIContent(text)).x;
            GUILayout.Label(text, GUILayout.Width(width));
        }
        public static void FixedLabel(string text, int fontSize, Color color)
        {
            var width = GUI.skin.label.CalcSize(new GUIContent(text)).x * ((float)fontSize / 10);
            FontLabel(text, fontSize, color, GUILayout.Width(width));
        }

        public static void FontLabel(string text, int fontSize, Color color, params GUILayoutOption[] options)
        {
            var style = new GUIStyle();
            style.fontSize = fontSize;
            style.normal.textColor = color;

            GUILayout.Label(text, style, options);
        }

        public static string TextField(string title, string text, GUIStyle style)
        {
            GUILayout.Label(title, style);
            text = GUILayout.TextField(text);
            GUILayout.Space(10);
            return text;
        }

        public static float HorizontalSlider(string title, float value, float min, float max)
        {
            GUILayout.Label(title);
            value = GUILayout.HorizontalSlider(value, min, max);
            GUILayout.Space(10);
            return value;
        }

        public static bool Button(string title, float width, float height)
        {
            return GUILayout.Button(title, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));
        }

        public static bool SpriteButton(Sprite sprite, float width, float height)
        {
            return GUILayout.Button(GraphicsHelper.Sprite2Texture(sprite), GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));
        }

        public static void HorizontalSplitter(int height)
        {
            GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(height));
        }
        public static void HorizontalSplitter(string title, int height)
        {
            GUILayout.Label(title);
            HorizontalSplitter(height);
        }

        public static bool DrawAddRemoveButtons<T>(List<T> list, T item, int index, Color color, int width = 20)
        {
            GUI.backgroundColor = color;
            if (GUILayout.Button("+", GUILayout.Width(width)))
            {
                list.Insert(index + 1, item);
                return true;
            }
            if (GUILayout.Button("-", GUILayout.Width(width)))
            {
                list.Remove(list[index]);
                return true;
            }
            GUI.backgroundColor = Color.white;

            return false;
        }
    }
}
