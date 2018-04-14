using System;
using System.Diagnostics;

namespace Akatsuki
{
    public class BenchmarkHelper
    {
        private Stopwatch m_stopWatch;
        private string m_tag;

        public BenchmarkHelper(string tag)
        {
            m_stopWatch = new Stopwatch();
            m_stopWatch.Start();
            m_tag = tag;
        }

        public void Finish()  
        {
            m_stopWatch.Stop();
            var elapsed = m_stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}.{2:000}",
            elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
            UnityEngine.Debug.Log(m_tag + " benchmark: " + elapsedTime);
        }
    }
}