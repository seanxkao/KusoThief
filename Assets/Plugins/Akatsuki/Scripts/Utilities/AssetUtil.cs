using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Akatsuki
{
    public class AssetUtil
    {
        public static T LoadOrCreateAsset<T>(string path) where T : ScriptableObject
        {
            var result = Resources.Load<T>(path);

            if (result == null)
            {
#if UNITY_EDITOR
                path = path.Substring(0, path.LastIndexOf('/'));
                result = CreateAsset<T>("Assets/Resources/" + path, typeof(T).Name);
#else
            result = ScriptableObject.CreateInstance<T>();
#endif
            }
            return result;
        }
#if UNITY_EDITOR
        public static T CreateAsset<T>(string path, string name) where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets";
            }

            if (!name.EndsWith(".asset", System.StringComparison.Ordinal))
            {
                name += ".asset";
            }

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name);
            Debug.Log("Create Asset:" + assetPathAndName);
            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();

            return asset;
        }
#endif
    }

}