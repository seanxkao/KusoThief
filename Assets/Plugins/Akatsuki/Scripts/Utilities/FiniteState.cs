using UnityEngine;

namespace Akatsuki
{
    public class FiniteState<T>
    {
        private T m_state;

        private T m_nextState;

        private bool m_transitting;

        private bool m_entering;

        private float m_enterTime = 0;

        public FiniteState(T state)
        {
            this.Init(state);
        }

        public T Current
        {
            get
            {
                lock (this)
                {
                    return m_state;
                }
            }
        }

        public bool Entering
        {
            get
            {
                lock (this)
                {
                    return m_entering;
                }
            }
        }

        public float EnterTime
        {
            get
            {
                return m_enterTime;
            }
        }

        public void Transit(T newState)
        {
            lock (this)
            {
                m_nextState = newState;
                m_transitting = true;
            }
        }

        public T Tick()
        {
            lock (this)
            {
                if (m_transitting)
                {
                    m_enterTime = Time.time;
                    m_state = m_nextState;
                    m_transitting = false;
                    m_entering = true;
                }
                else
                {
                    m_entering = false;
                }

                return m_state;
            }
        }

        public void ResetEnterTime()
        {
            m_enterTime = Time.time;
        }

        private void Init(T state)
        {
            m_state = state;
            m_nextState = state;
            m_transitting = true;
            m_entering = false;
        }
    }
}
