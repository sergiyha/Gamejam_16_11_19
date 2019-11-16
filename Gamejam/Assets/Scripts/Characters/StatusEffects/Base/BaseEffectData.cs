using System;
using System.Linq;
using System.Reflection;
using Characters.StatusEffects;
using UnityEngine;

public class BaseEffectData : ScriptableObject
{
    [EffectImplementation]
    [SerializeField]
    private string imp;
        
    #region EffectsFactory

    private static Type[] allEffects;
        
    public static StatusEffectBase Create(BaseEffectData effectData, Character character)
    {
        var t = Activator.CreateInstance(FindEffect(effectData.imp), effectData, character);
        return (StatusEffectBase) t;
    }

    public static Type FindEffect(string name)
    {
        if (allEffects == null)
        {
            string nspace = typeof(Bleed).Namespace;

            var q = (from t in Assembly.GetAssembly(typeof(Bleed)).GetTypes()
                where t.IsClass && t.Namespace != null && t.Namespace.Contains(nspace)
                select t).ToList();

            for (int i = 0; i < q.Count; i++)
            {
                var baseT = q[i].BaseType;
                bool quit = false;
                while (!quit)
                {
                    if (baseT == null)
                    {
                        q.RemoveAt(i);
                        i--;
                        quit = true;
                    }
                    else if (baseT == typeof(StatusEffectBase))
                        quit = true;
                    else
                        baseT = baseT.BaseType;
                }
            }

            allEffects = q.ToArray();
        }

        return allEffects.FirstOrDefault(e => e.Name == name);
    }

    #endregion
}