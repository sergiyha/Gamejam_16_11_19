using System;
using System.Collections.Generic;
using System.Linq;
using LifelongAdventure.Helpers.SerializableDictionary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FPSTestProject.Helpers.Runtime.SoundManager
{
    [CreateAssetMenu(fileName = "SoundManagerDatabase", menuName = "SoundManagerDatabase", order = 999)]
    public class SoundManagerDatabase : ScriptableObject
    {
        private static SoundManagerDatabase instance;

        public static SoundManagerDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<SoundManagerDatabase>("SoundManager/SoundManagerDatabase");
                }

                return instance;
            }
        }

        [SerializeField]
        private SoundTypedDictionary database;

        public static AudioClip GetRandomClip(SoundType type)
        {
            if (Instance.database.ContainsKey(type))
            {
                return instance.database[type].GetRandom();
            }

            return null;
        }
        public static AudioClip[] GetClips(SoundType type)
        {
            if (Instance.database.ContainsKey(type))
            {
                return instance.database[type].All;
            }

            return null;
        }

        [Serializable]
        private class SoundTypedDictionary : SerializableDictionary<SoundType, AudioHolder>
        { }

        [Serializable]
        private class AudioHolder
        {
            [SerializeField]
            private AudioClip[] clips;

            public AudioClip[] All => clips;

            public AudioClip GetRandom()
            {
                return clips[Random.Range(0, clips.Length - 1)];
            }
        }

        #if UNITY_EDITOR

        [ContextMenu("AddAll&Sort")]
        public void AddAll()
        {
            var allEnums = Enum.GetValues(typeof(SoundType));
            foreach (var cEnum in allEnums)
            {
                if (!database.ContainsKey((SoundType) cEnum))
                {
                    database.Add((SoundType) cEnum, new AudioHolder());
                }
            }

            var newDict = database.OrderBy(e => (int) e.Key).ToArray();
            database = new SoundTypedDictionary();

            foreach (KeyValuePair<SoundType, AudioHolder> holder in newDict)
            {
                database.Add(holder);
            }
        }

        #endif

    }
}
