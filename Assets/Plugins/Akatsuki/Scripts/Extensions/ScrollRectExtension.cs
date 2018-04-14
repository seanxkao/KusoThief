using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Akatsuki;


public static partial class ExtensionMethods
{
    ///<summary>
    /// refreshes scroll rect's content with a new list on a coroutine
    ///</summary>
    public static List<T> GetList<T>(this ScrollRect scrollRect)
    {
        return scrollRect.content.GetComponentsInFirstDepth<T>();
    }

    ///<summary>
    /// refreshes scroll rect's content with a new list on a coroutine
    ///</summary>
    public static void RefreshList<T>(this ScrollRect scrollRect, GameObject cellPrefab, int count, Action<List<T>> onComplete) where T : MonoBehaviour
    {
        scrollRect.StartCoroutine(RefreshList<T>(scrollRect.content, cellPrefab, count, onComplete));
    }

    private static IEnumerator RefreshList<T>(Transform root, GameObject cellPrefab, int count, Action<List<T>> onComplete) where T : MonoBehaviour
    {
        root.DestroyAllChildren();

        while (root.childCount > 0) { yield return null; }

        var list = new List<T>();

        count.Loop((obj) => { list.Add(GameObject.Instantiate(cellPrefab, root).GetComponent<T>()); });

        onComplete(list);
    }
}
