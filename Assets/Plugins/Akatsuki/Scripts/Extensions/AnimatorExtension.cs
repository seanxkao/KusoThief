using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class ExtensionMethods
{
    public static void PlayAnimation(this GameObject obj, string name)
    {
        var animator = obj.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.Play(name);
        }
    }

    public static IEnumerator Play(this Animator animator, string name, Action callback)
    {
        yield return Play(animator, name);

        if (callback != null)
        {
            callback();
        }
    }

    public static float AnimatorLength(this GameObject obj)
    {
        var animator = obj.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }
        return 0;
    }

    public static IEnumerator WaitUntilFinished(this Animator animator)
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
    }

    private static IEnumerator Play(this Animator animator, string name)
    {
        if (!animator.HasState(0, Animator.StringToHash(name)))
        {
            Debug.LogWarning("Animation State:" + name + " not found");
            yield break;
        }
        animator.Play(name, 0, 0);
        yield return animator.WaitUntilFinished();
    }

    private static IEnumerator Play(this Animator animator, int hash)
    {
        if (!animator.HasState(0, hash))
        {
            Debug.LogWarning("Animation State:" + hash + " not found");
            yield break;
        }
        animator.Play(hash, 0, 0);
        yield return animator.WaitUntilFinished();
    }

    public static IEnumerator PlayAndResume(this Animator animator, string name, Action callback = null)
    {
        var originState = GetNameHash(animator);

        yield return Play(animator, name);

        animator.Play(originState, 0, 0);

        if (callback != null)
        {
            callback();
        }
    }

    public static IEnumerator Restart(this Animator animator, string state, Action callback = null)
    {
        if (animator != null)
        {
            animator.Play(state, 0, 0);
        }

        yield return animator.WaitUntilFinished();

        if (callback != null)
        {
            callback();
        }
    }

    public static IEnumerator Restart(this Animator animator, Action callback)
    {
        if (animator != null)
        {
            animator.JumpTo(0);
        }

        yield return animator.WaitUntilFinished();

        if (callback != null)
        {
            callback();
        }
    }

    public static void RestartAnimation(this GameObject obj)
    {
        var animator = obj.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.JumpTo(0);
        }
    }

    public static IEnumerator RestartAnimation(this GameObject obj, Action callback)
    {
        var animator = obj.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            yield return animator.Restart(callback);
        }

        yield return null;
    }

    public static void JumpTo(this Animator animator, float time)
    {
        animator.Play(GetNameHash(animator), 0, time);
    }

    public static int GetNameHash(this Animator animator)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.fullPathHash;
    }

    public static void PlayAndReturn<T>(this Animator animator, MonoBehaviour instance, T state)
    {
        animator.PlayAndReturn(instance, state.ToString());
    }
    public static void PlayAndReturn(this Animator animator, MonoBehaviour instance, string name)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return;
        }

        var hashes = new List<int>();
        hashes.Add(Animator.StringToHash(name));
        hashes.Add(animator.GetNameHash());

        instance.StartCoroutine(animator.PlaySequence(hashes));
    }

    public static void PlaySequence<T>(this Animator animator, MonoBehaviour instance, params T[] states)
    {
        var names = new List<string>();
        foreach (var state in states)
        {
            names.Add(state.ToString());
        }
        instance.StartCoroutine(animator.PlaySequence(names));
    }
    public static void PlaySequence(this Animator animator, int a, MonoBehaviour instance, params string[] names)
    {
        instance.StartCoroutine(animator.PlaySequence(names.ToList()));
    }

    public static IEnumerator PlaySequence(this Animator animator, List<string> names, Action callback = null)
    {
        foreach (var name in names)
        {
            yield return Play(animator, name);
        }

        if (callback != null)
        {
            callback();
        }

        yield return null;
    }

    public static IEnumerator PlaySequence(this Animator animator, List<int> hashes, Action callback = null)
    {
        foreach (var hash in hashes)
        {
            yield return Play(animator, hash);
        }

        if (callback != null)
        {
            callback();
        }

        yield return null;
    }
}
