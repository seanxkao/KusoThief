using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace Akatsuki
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly bool IS_DEBUG_LOG = false;

        private static T instance;
        private static object mutex = new object();

        public static T I
        {
            get
            {
                if (applicationIsQuitting)
                {
                    DebugLog("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }

                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (HasCreated)
                        {
                            DebugLog("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopenning the scene might fix it.");
                            return instance;
                        }

                        if (instance == null)
                        {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).Name.ToString();

                            DontDestroyOnLoad(singleton);

                            DebugLog("[Singleton] An instance of " + typeof(T) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            DebugLog("[Singleton] Using instance already created: " +
                                instance.gameObject.name);
                        }
                    }

                    return instance;
                }
            }
        }

        public static bool HasCreated
        {
            get
            {
                return FindObjectsOfType(typeof(T)).Length > 0;
            }
        }

        private static void DebugLog(string log)
        {
            if (IS_DEBUG_LOG)
            {
                Debug.Log(log);
            }
        }

        private static bool applicationIsQuitting = false;
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed,
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        protected virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}
