using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akatsuki
{
    public class CoroutineManager : MonoSingleton<CoroutineManager>
    {
        private List<Action> m_actions = new List<Action>();

        public void AddAction(Action action)
        {
            m_actions.Add(action);
        }

        public void RemoveAction(Action action)
        {
            if (m_actions.Contains(action))
            {
                m_actions.Remove(action);
            }
        }

        public void ClearActions()
        {
            m_actions.Clear();
        }

        public void Update()
        {
            for (int i = 0; i < m_actions.Count; ++i)
            {
                m_actions[i]();
            }
        }

        public Coroutine Run(string methodName)
        {
            return StartCoroutine(methodName);
        }

        public Coroutine Run(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void Stop(IEnumerator routine)
        {
            StopCoroutine(routine);
        }
    }
}
