using UnityEngine;
using System.Collections.Generic;

public static partial class ExtensionMethods
{
    public static GameObject AddEmptyChild(this GameObject parent)
    {
        var obj = new GameObject();
        SetParent(parent, obj);

        var t = obj.transform;
        t.localPosition = Vector3.zero;
        t.localScale = Vector3.one;

        return obj;
    }

    public static GameObject AddChild(this GameObject parent, GameObject prefab)
    {
        var obj = GameObject.Instantiate(prefab) as GameObject;
        SetParent(parent, obj);

        var t = obj.transform;
        t.localPosition = prefab.transform.localPosition;
        t.localRotation = prefab.transform.localRotation;
        t.localScale = prefab.transform.localScale;

        return obj;
    }

    public static GameObject AddChild(this GameObject parent, Transform child)
    {
        SetParent(parent, child.gameObject);

        child.localPosition = Vector3.zero;
        child.localRotation = Quaternion.identity;
        child.localScale = Vector3.one;

        return child.gameObject;
    }

    private static GameObject SetParent(GameObject parent, GameObject child)
    {
        if (parent != null)
        {
            child.transform.SetParent(parent.transform);
            child.layer = parent.layer;
        }
        return child;
    }

    public static GameObject AddChild(this GameObject prefab)
    {
        return AddChild(null, prefab);
    }

    public static T AddChild<T>(this GameObject prefab) where T : Component
    {
        var obj = AddChild(null, prefab);
        var returnClass = obj.AddOrGetComponent<T>();
        return returnClass;
    }

    public static T AddChild<T>(this GameObject parent, GameObject prefab) where T : Component
    {
        var obj = AddChild(parent, prefab);
        var returnClass = obj.AddOrGetComponent<T>();
        return returnClass;
    }

    public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject.GetComponent<T>() == null)
            return gameObject.AddComponent<T>();
        else
            return gameObject.GetComponent<T>();
    }

    public static void Destroy(this IList<GameObject> list)
    {
        if (list == null)
        {
            return;
        }

        foreach (var item in list)
        {
            Object.Destroy(item);
        }
        list.Clear();
    }

    public static void Destroy(this GameObject obj)
    {
        Object.Destroy(obj);
    }

    public static void DestroyChild(this GameObject obj)
    {
        foreach (Transform t in obj.transform)
        {
            t.gameObject.Destroy();
        }
    }
}
