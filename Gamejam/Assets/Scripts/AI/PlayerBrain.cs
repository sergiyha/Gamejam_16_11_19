using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
	private Character _someCharacter;
	private PlayerController _playerController;

	private void Start()
	{
		_playerController = GetComponent<PlayerController>();
		StartCoroutine(CheckAction());
	}

	private bool _melleeAttakingNow;
	private bool _rangeAttakingNow;

	private IEnumerator CheckAction()
	{
		while (true)
		{
			yield return null;
			List<Character> enemies = null;
			if (!Character.Characters.TryGetValue(Character.CharType.Bot, out enemies))
			{
				_playerController.OnStartMeleeAttack(null);
				_playerController.OnStartRangeAttack(null);
				continue;
			}


			var meleDistance = _playerController.GetMeleeDistance();
			var rangeDistance = _playerController.GetRangeDistance();


			List<CharacterAndDistance> CharMeleDist = null;
			List<CharacterAndDistance> CharRangedDist = null;
			var meleeTargets = enemies.Where(e => Vector3.Distance(this.transform.position, e.transform.position) <= meleDistance);
			var rangedTargets = enemies.Where(e => Vector3.Distance(this.transform.position, e.transform.position) <= rangeDistance);
			enemies.ForEach(e =>
			{
				var dist = Vector3.Distance(this.transform.position, e.transform.position);

				if (dist <= meleDistance)
				{
					CharMeleDist = CharMeleDist ?? new List<CharacterAndDistance>();
					CharMeleDist.Add(new CharacterAndDistance()
					{
						Character = e,
						Distance = dist
					});
				}
				 if (dist <= rangeDistance)
				{
					CharRangedDist = CharRangedDist ?? new List<CharacterAndDistance>();
					CharRangedDist.Add(new CharacterAndDistance()
					{
						Character = e,
						Distance = dist
					});
				}


			});

			_playerController.OnStartRangeAttack(CharRangedDist);
			_playerController.OnStartMeleeAttack(CharMeleDist);


		}
	}
}
