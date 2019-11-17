using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FPSTestProject.Helpers.Runtime.ObjectPool
{
    public abstract class PoolInitializer
    {
        private static Dictionary<Type, MonoBehaviour> pools = new Dictionary<Type, MonoBehaviour>();

        private static Dictionary<Type, Type> poolableToPool = new Dictionary<Type, Type>();

        public static ObjectPool<T> GetPool<T>() where T : IPoolable
        {
            var needableType = typeof(T);
            if (pools.ContainsKey(needableType))
            {
                return (ObjectPool<T>)pools[needableType];
            }

            pools.Add(needableType, (MonoBehaviour)new GameObject($"{needableType.Name}Pool").AddComponent(poolableToPool[needableType]));
            return (ObjectPool<T>)pools[needableType];
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeType()
        {
            var q = (from t in AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                     where t.IsClass && t.BaseType != null && t.BaseType.Name.Contains("ObjectPool") && t.Namespace != null
                select t).ToList();

            foreach (Type type in q)
            {
                poolableToPool.Add(type.BaseType.GetGenericArguments()[0], type);
            }
        }

        public static bool Exist(Type type)
        {
            return pools.ContainsKey(type.BaseType.GetGenericArguments()[0]);
        }

        public static void CleanUpPool<T>() where T : IPoolable
        {
            pools.Remove(typeof(T));
        }
    }
}
