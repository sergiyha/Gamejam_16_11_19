using System;
using System.Collections.Generic;
using LifelongAdventure.Creatures.Data;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Dictionary<CharType, List<Character>> Characters = new Dictionary<CharType, List<Character>>();

    public CharType CharacterType;
    public CreatureStats Stats;

    public HealthController HealthController;
    public CharacterAnimationController AnimationController;
    public WeaponController WeaponController;
    public StatusEffectsController StatusEffectsController;
    public AudioSource AudioSource;

    [NonSerialized]
    public CreatureStats InitialStats;

    private void OnDisable()
    {
        if (!Characters.ContainsKey(CharacterType))
            Characters.Add(CharacterType, new List<Character>());

        Characters[CharacterType].Add(this);
    }

    private void OnEnable()
    {
        InitialStats = CreatureStats.Clone(Stats);

        if (Characters.ContainsKey(CharacterType))
            Characters[CharacterType].Remove(this);
        StatusEffectsController = GetComponent<StatusEffectsController>();
        AudioSource = GetComponent<AudioSource>();
        WeaponController = GetComponent<WeaponController>();
        HealthController = GetComponent<HealthController>();
    }

    public enum CharType
    {
        Player = 0,
        Bot = 1
    }
}
