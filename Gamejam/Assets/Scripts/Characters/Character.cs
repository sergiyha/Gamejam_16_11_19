using System;
using System.Collections.Generic;
using AI;
using Characters.Controllers;
using LifelongAdventure.Creatures.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	public static Dictionary<CharType, List<Character>> Characters = new Dictionary<CharType, List<Character>>();

    public event Action<Character> OnDead = delegate { };

	public CharType CharacterType;
	public Sprite Icon;

    public bool IsDead { get; protected set; }

	public CreatureStats Stats;

    [NonSerialized]
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
		return WeaponController.Weapon.Range > 5;
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
		WeaponController = GetComponent<WeaponController>();
		HealthController = GetComponent<HealthController>();
		ArctifactsController = GetComponent<ArctifactsController>();
		AimingModule = GetComponent<PlayerAimModule>();
		//WeaponController.AddWeapon(debugWeapon);
	}

    [Button]
    public void DoDeath()
    {
        if (Characters.ContainsKey(CharacterType))
            Characters[CharacterType].Remove(this);

        IsDead = true;
        AnimationController.DoDeath();
        GetComponent<NavMeshAgent>().enabled = false;

        StatusEffectsController.enabled = false;
        WeaponController.enabled = false;
        if (ArctifactsController != null)
            ArctifactsController.enabled = false;
        if (AimingModule != null)
            AimingModule.enabled = false;

        transform.parent = null;
        OnDead(this);
        Destroy(gameObject, 5);

        if (CharacterType == CharType.Bot)
            GetComponent<BotBrain>().enabled = false;
    }
}
