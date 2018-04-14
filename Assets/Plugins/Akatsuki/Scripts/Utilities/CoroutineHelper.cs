using System;
using System.Collections;
using UnityEngine;

namespace Akatsuki
{
    public class CoroutineHelper
    {
        public delegate Result Function<Result>();

        public static IEnumerator Delay(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        public static IEnumerator Chain(MonoBehaviour instance, params IEnumerator[] actions)
        {
            foreach (IEnumerator action in actions)
            {
                yield return instance.StartCoroutine(action);
            }
        }

        public static IEnumerator Do(Action action)
        {
            action();
            yield return 0;
        }

        public static IEnumerator Until<Result>(Function<Result> action, Result result)
        {
            while (!action().Equals(result))
            {
                yield return null;
            }
        }
    }
}
