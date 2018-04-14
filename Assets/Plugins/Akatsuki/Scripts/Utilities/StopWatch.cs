using System;
using System.Collections.Generic;
using UnityEngine;

namespace Akatsuki
{
    public class StopWatch
    {
        private float m_time = 0;
        private List<float> m_ticks = null;

        public StopWatch()
        {
            m_ticks = new List<float>();

            Start();
        }

        public void Start()
        {
            m_time = Time.time;
        }

        public void Stop()
        {
            m_ticks.Add(Time.time - m_time);
        }

        public float Total
        {
            get
            {
                float total = 0;
                foreach (var tick in m_ticks)
                {
                    total += tick;
                }

                return total;
            }
        }
    }
}
