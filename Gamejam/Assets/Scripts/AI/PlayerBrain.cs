using System.Collections;
using System.Collections.Generic;
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
			if (!Character.Characters.TryGetValue(Character.CharType.Bot, out enemies)) continue;

			var meleDistance = _playerController.GetMeleeDistance();
			var rangeDistance = _playerController.GetRangeDistance();


			for (int i = 0; i < enemies.Count; i++)
			{
				var enemy = enemies[i];
				var distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
				if (distanceToEnemy <= meleDistance)
				{
					if (!_melleeAttakingNow)
					{
						_melleeAttakingNow = true;
						_playerController.OnStartMeleeAttack(enemy);
					}
				}
				else
				{
					_melleeAttakingNow = false;
				}

				if (distanceToEnemy <= rangeDistance)
				{
					if (!_rangeAttakingNow)
					{
						_rangeAttakingNow = true;
						_playerController.OnStartRangeAttack(enemy);
					}
				}
				else
				{
					_rangeAttakingNow = false;
				}
			}
		}
	}
}
