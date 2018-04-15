using System;
using System.Collections;
using UnityEngine;

namespace Akatsuki
{
    public class Singleton<T> where T : class
    {
        private static T m_instance = null;

        public static T I
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = CreateInstance();
                }
                return m_instance;
            }
        }

        private static T CreateInstance()
        {
            Type type = typeof(T);
            return Activator.CreateInstance(type, true) as T;
        }
    }

    public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        private static T m_instance;
        public static T I
        {
            get
            {
                if (m_instance == null)
                {
                    T t = ScriptableObject.CreateInstance<T>();                    
                    m_instance = t.CreateInstance();
                }
                return m_instance;
            }
        }

        public abstract string AssetPath();

        private T CreateInstance()
        {
            return AssetUtil.LoadOrCreateAsset<T>(AssetPath());
        }

        public static void Load()
        {
            m_instance = I;
        }

        public static void Unload()
        {
            m_instance = null;
        }
    }
}
