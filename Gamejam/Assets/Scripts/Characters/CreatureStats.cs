using System;
using System.Collections.Generic;
using LifelongAdventure.Helpers.SerializableDictionary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LifelongAdventure.Creatures.Data
{
    [Serializable]
    public struct CreatureStats
    {
        [SerializeField]
        private StatToIntDictionary allStats;

        public int this[Stat stat]
        {
            get
            {
                if (allStats != null)
                {
                    if (allStats.ContainsKey(stat))
                        return allStats[stat];
                }

                return 0;
            }
            set
            {
                if(allStats == null)
                    allStats = new StatToIntDictionary();
                
                allStats[stat] = value;
            }
        }

        public static CreatureStats operator +(CreatureStats s1, CreatureStats s2)
        {
            if(s1.allStats == null)
                s1.allStats = new StatToIntDictionary();

            if (s2.allStats == null)
                return s1;

            var allValues = Enum.GetValues(typeof(Stat));
            foreach (Stat value in allValues)
            {
                s1[value] += s2[value];
            }
            
            return s1;
        }

        public static CreatureStats Clone(CreatureStats from)
        {
            CreatureStats newStats = new CreatureStats();
            newStats.allStats = new StatToIntDictionary();

            foreach (KeyValuePair<Stat, int> pair in from.allStats)
            {
                newStats.allStats.Add(pair);
            }

            return newStats;
        }

        [Serializable]
        public class StatToIntDictionary : SerializableDictionary<Stat, int>
        {}

#if UNITY_EDITOR
        [Button]
        private void AddAllStats()
        {
            var allValues = Enum.GetValues(typeof(Stat));
            foreach (Stat value in allValues)
            {
                if (!allStats.Contains(value))
                    allStats.Add(value, 0);
            }
        }
#endif
    }
}