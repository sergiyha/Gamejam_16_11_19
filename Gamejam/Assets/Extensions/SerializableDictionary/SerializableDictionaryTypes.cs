using System;

namespace LifelongAdventure.Helpers.SerializableDictionary
{
    [Serializable]
    public class StringGameObjectDictionary : SerializableDictionary<string, UnityEngine.Object>
    {
    }
    
    [Serializable]
    public class StringStringDictionary : SerializableDictionary<string, string>
    {
    }

    [Serializable]
    public class StringFloatDictionary : SerializableDictionary<string, float>
    {
    }
}