using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPSTestProject.Helpers.Runtime.ObjectPool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : IPoolable
    {
        protected abstract bool KeepBetweenLevels { get; }

        protected abstract int InitialCount { get; }

        protected abstract string Prefab { get; }

        private T[] pool;

        protected virtual void Awake()
        {
            if (PoolInitializer.Exist(GetType()))
            {
                Destroy(gameObject);
            }

            pool = new T[InitialCount];
            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = CreateObject();
                pool[i].Deactivate();
            }

            if (KeepBetweenLevels)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public T GetObject(Vector3 position, Quaternion rotation)
        {
            //TODO: If object was attached and destroy - it's not null and i didn't find a way to check it without try
            if (KeepBetweenLevels)
            {
                int lastIndex = 0;
                try
                {
                    for (int i = 0; i < pool.Length; i++)
                    {
                        lastIndex = i;
                        if (pool[i] == null)
                        {
                            pool[i] = CreateObject();
                            pool[i].Activate(position, rotation);
                            return pool[i];
                        }

                        if (!pool[i].IsActive)
                        {
                            pool[i].Activate(position, rotation);
                            return pool[i];
                        }
                    }
                }
                catch
                {
                    pool[lastIndex] = CreateObject();
                    pool[lastIndex].Activate(position, rotation);
                    return pool[lastIndex];
                }
            }
            else
                for (int i = 0; i < pool.Length; i++)
                {
                    if (!pool[i].IsActive)
                    {
                        pool[i].Activate(position, rotation);
                        return pool[i];
                    }
                }

            var oldLength = pool.Length;
            Array.Resize(ref pool, pool.Length + InitialCount);
            pool[oldLength] = CreateObject();

            for (int i = oldLength + 1; i < pool.Length; i++)
            {
                pool[i] = CreateObject();
                pool[i].Deactivate();
            }

            pool[oldLength].Activate(position, rotation);
            return pool[oldLength];
        }

        private T CreateObject()
        {
            return Instantiate(Resources.Load<GameObject>(Prefab), transform).GetComponent<T>();
        }

        private void OnDestroy()
        {
            //Dispose
            //foreach (T poolable in pool)
            //{
            //    poolable.Deactivate();
            //}

            PoolInitializer.CleanUpPool<T>();
        }
    }
}
