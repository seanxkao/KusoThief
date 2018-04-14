using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Akatsuki
{
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private const int DEFAULT_CAPACITY = 8;

        private Dictionary<string, Queue<GameObject>> pools =
            new Dictionary<string, Queue<GameObject>>(
                DEFAULT_CAPACITY
            );
        private Dictionary<string, GameObject> parents =
            new Dictionary<string, GameObject>(
                DEFAULT_CAPACITY
            );
        private Dictionary<string, GameObject> prefabs =
            new Dictionary<string, GameObject>(
                DEFAULT_CAPACITY
            );

        public void AddPool(string type, GameObject parent, GameObject prefab, int count)
        {
            if (!pools.ContainsKey(type))
            {
                pools.Add(type, new Queue<GameObject>());
                parents.Add(type, parent);
                prefabs.Add(type, prefab);

                for (int i = 0; i < count; ++i)
                {
                    Create(type, parent, prefab);
                }
            }
        }

        public GameObject Get(string type)
        {
            if (!pools.ContainsKey(type))
            {
                return null;
            }

            if (!pools[type].Any())
            {
                Create(type, parents[type], prefabs[type]);
            }

            var obj = pools[type].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        private void Create(string type, GameObject parent, GameObject prefab)
        {
            var obj = parent.AddChild(prefab);
            obj.name = prefab.name + " (Pool)";
            obj.SetActive(false);

            pools[type].Enqueue(obj);
        }

        public void Recycle(string type, GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.name = type + " (Pool)";
            obj.SetActive(false);
            pools[type].Enqueue(obj);
        }

        public void Clear()
        {
            var keys = pools.Keys.ToList();
            foreach (var key in keys)
            {
                Clear(key);
            }

            pools.Clear();
        }

        public void Clear(string type)
        {
            if (pools.ContainsKey(type))
            {
                foreach (var obj in pools[type])
                {
                    Destroy(obj);
                }
                pools[type].Clear();

                parents.Remove(type);
                prefabs.Remove(type);
                pools.Remove(type);
            }
        }
    }
}
