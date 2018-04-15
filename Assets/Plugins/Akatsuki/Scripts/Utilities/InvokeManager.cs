using System;
using System.Collections.Generic;
using UnityEngine;

namespace Akatsuki
{
    public class InvokeManager : MonoSingleton<InvokeManager>
    {
        private Dictionary<object, Invoker> m_invokers = null;

        void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_invokers = new Dictionary<object, Invoker>();
        }

        private void AddInvoker(object obj, Action callback, Timer timer)
        {
            if (!m_invokers.ContainsKey(obj))
            {
                var invoker = new Invoker();
                invoker.Add(timer);
                m_invokers.Add(obj, invoker);
            }
            else
            {
                var invoker = m_invokers[obj];
                invoker.Add(timer);
            }
        }

        private void AddInvoker(object obj, Action<object[]> callback, Timer timer)
        {
            if (!m_invokers.ContainsKey(obj))
            {
                var invoker = new Invoker();
                m_invokers.Add(obj, invoker);
                invoker.Add(timer);
            }
            else
            {
                var invoker = m_invokers[obj];
                invoker.Add(timer);
            }
        }

        public void Invoke(object obj, Action callback, float time)
        {
            Timer timer = this.gameObject.AddComponent<Timer>();
            timer.Invoke(callback, time);

            AddInvoker(obj, callback, timer);
        }

        public void Invoke(object obj, Action<object[]> callback, float time, params object[] parms)
        {
            Timer timer = this.gameObject.AddComponent<Timer>();
            timer.Invoke(callback, time, parms);

            AddInvoker(obj, callback, timer);
        }

        public void InvokeRepeating(object obj, Action callback, float time, float repeatRate)
        {
            Timer timer = this.gameObject.AddComponent<Timer>();
            timer.InvokeRepeating(callback, time, repeatRate);

            AddInvoker(obj, callback, timer);
        }

        public void InvokeRepeating(object obj, Action<object[]> callback, float time, float repeatRate, params object[] parms)
        {
            Timer timer = this.gameObject.AddComponent<Timer>();
            timer.InvokeRepeating(callback, time, repeatRate, obj);

            AddInvoker(obj, callback, timer);
        }

        public static void Cancel(object obj)
        {
            if (I == null)
            {
                return;
            }

            I.CancelInvoke(obj);
        }

        private void CancelInvoke(object obj)
        {
            if (m_invokers.ContainsKey(obj))
            {
                m_invokers[obj].Clear();
                m_invokers.Remove(obj);
            }
        }

        public static void RemoveGameObject(object[] objects)
        {
            foreach (var obj in objects)
            {
                GameObject gameObject = (GameObject)obj;
                UnityEngine.Object.Destroy(gameObject);
            }
        }

        public void Clear()
        {
            foreach (var invoker in m_invokers.Values)
            {
                invoker.Clear();
            }
            m_invokers.Clear();
        }
    }

    internal class Invoker
    {
        public IList<Timer> Timers = new List<Timer>();

        internal void Add(Timer timer)
        {
            Timers.Add(timer);
        }

        internal void Clear()
        {
            foreach (var timer in Timers)
            {
                timer.Clear();
            }
            Timers.Clear();
        }
    }

    internal class Timer : MonoBehaviour
    {
        private Action m_callback = null;
        private Action<object[]> m_callbackParams = null;
        private object[] m_params = null;
        private bool m_isClear = false;

        internal void Invoke(Action callback, float time)
        {
            m_callback = callback;

            Invoke("InvokeTrigger", time);
        }

        private void InvokeTrigger()
        {
            if (m_callback != null)
            {
                m_callback();
            }
            Clear();
        }

        internal void Invoke(Action<object[]> callback, float time, params object[] obj)
        {
            m_callbackParams = callback;
            m_params = obj;

            Invoke("InvokeTriggerParams", time);
        }

        private void InvokeTriggerParams()
        {
            m_callbackParams(m_params);
            Clear();
        }

        internal void InvokeRepeating(Action callback, float time, float repeatRate)
        {
            m_callback = callback;

            InvokeRepeating("InvokeRepeatingTrigger", time, repeatRate);
        }

        private void InvokeRepeatingTrigger()
        {
            if (m_callback != null)
            {
                m_callback();
            }
        }

        internal void InvokeRepeating(Action<object[]> callback, float time, float repeatRate, params object[] obj)
        {
            m_callbackParams = callback;

            InvokeRepeating("InvokeRepeatingTriggerParams", time, repeatRate);
        }

        private void InvokeRepeatingTriggerParams()
        {
            m_callbackParams(m_params);
        }

        internal void Clear()
        {
            if (m_isClear)
            {
                return;
            }

            CancelInvoke();
            Destroy(this);

            m_isClear = true;
        }
    }
}
