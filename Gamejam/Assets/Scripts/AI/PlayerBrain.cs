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

	private IEnumerator CheckAction()
	{
		while (true)
		{
			List<Character> enemies = null;
			if (!Character.Characters.TryGetValue(Character.CharType.Bot, out enemies)) continue;

			var meleDistance = _playerController.GetMeleeDistance();
			var rangeDistance = _playerController.GetRangeDistance();
			yield return null;

			for (int i = 0; i < enemies.Count; i++)
			{
				var enemy = enemies[i];
				var distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
				if (distanceToEnemy <= meleDistance)
				{
					_playerController.OnStartMeleeAttack(enemy);
				}

				if (distanceToEnemy <= rangeDistance)
				{
					_playerController.OnStartRangeAttack(enemy);
				}
			}
		}
	}
}
