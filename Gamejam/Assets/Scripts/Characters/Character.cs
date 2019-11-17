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

	public CreatureStats InitialStats;

	public HealthController HealthController;
	public CharacterAnimationController AnimationController;
	public WeaponController WeaponController;
	public StatusEffectsController StatusEffectsController;
	public UiController UiController;
	public ArctifactsController ArctifactsController;
	public AudioSource AudioSource;
	public PlayerAimModule AimingModule;

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

	public bool IsRanged()
	{
		//Debug.LogError(this.gameObject.name);
		return WeaponController.Weapon.Range > 3;
	}

	public void Awake()
	{
		StatusEffectsController = GetComponent<StatusEffectsController>();
		AudioSource = GetComponent<AudioSource>();
		WeaponController = GetComponent<WeaponController>();
		HealthController = GetComponent<HealthController>();
		ArctifactsController = GetComponent<ArctifactsController>();
		AimingModule = GetComponent<PlayerAimModule>();
	}

	private void OnEnable()
	{
		InitialStats = CreatureStats.Clone(Stats);
		if (!Characters.ContainsKey(CharacterType))
			Characters.Add(CharacterType, new List<Character>());

		Characters[CharacterType].Add(this);
		StatusEffectsController = GetComponent<StatusEffectsController>();
		AudioSource = GetComponent<AudioSource>();
		WeaponController = GetComponent<WeaponController>();
		HealthController = GetComponent<HealthController>();
		ArctifactsController = GetComponent<ArctifactsController>();
		AimingModule = GetComponent<PlayerAimModule>();
		//WeaponController.AddWeapon(debugWeapon);
	}
}
