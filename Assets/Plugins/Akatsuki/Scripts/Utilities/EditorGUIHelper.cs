#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Akatsuki
{
    public class EditorGUIHelper
    {
        public static int TitleIntField(string title, int number, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.IntField(number, options);
        }

        public static float TitleFloatField(string title, float number, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.FloatField(number, options);
        }

        public static string TitleTextField(string title, string text, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.TextField(text, options);
        }
    }
}

#endif