using System;
using System.Collections.Generic;
using Characters.Controllers;
using LifelongAdventure.Creatures.Data;
using UnityEngine;

public class Character : MonoBehaviour
{
	public static Dictionary<CharType, List<Character>> Characters = new Dictionary<CharType, List<Character>>();

	public CharType CharacterType;
	public Sprite Icon;

	public CreatureStats Stats;

    [NonSerialized]
	public CreatureStats InitialStats;

	public HealthController HealthController;
	public CharacterAnimationController AnimationController;
	public WeaponController WeaponController;
	public StatusEffectsController StatusEffectsController;
	public UiController UiController;
	public ArctifactsController ArctifactsController;

	public enum CharType
	{
		Player = 0,
		Bot = 1
	}

	private void OnDisable()
	{
		if (Characters.ContainsKey(CharacterType))
			Characters[CharacterType].Remove(this);

	}

	private void OnEnable()
	{
		InitialStats = CreatureStats.Clone(Stats);
		if (!Characters.ContainsKey(CharacterType))
			Characters.Add(CharacterType, new List<Character>());

		Characters[CharacterType].Add(this);
		StatusEffectsController = GetComponent<StatusEffectsController>();
		WeaponController = GetComponent<WeaponController>();
		HealthController = GetComponent<HealthController>();
		ArctifactsController = GetComponent<ArctifactsController>();
		//WeaponController.AddWeapon(debugWeapon);
	}
}
