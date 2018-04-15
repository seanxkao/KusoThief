using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static partial class ExtensionMethods
{
    public static T DeepClone<T>(this T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }

    public static T DeepJsonClone<T>(this T a)
    {
        var json = JsonUtility.ToJson(a);
        return JsonUtility.FromJson<T>(json);
    }
}
