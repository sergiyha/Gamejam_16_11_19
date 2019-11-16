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
	public AudioSource AudioSource;

	public enum CharType
	{
		Player = 0,
		Bot = 1
	}

	private void OnDisable()
	{
		if (!Characters.ContainsKey(CharacterType))
			Characters[CharacterType].Remove(this);
	}

	private void OnEnable()
	{
		if (!Characters.ContainsKey(CharacterType))
			Characters.Add(CharacterType, new List<Character>());

		Characters[CharacterType].Add(this);
	}


}
