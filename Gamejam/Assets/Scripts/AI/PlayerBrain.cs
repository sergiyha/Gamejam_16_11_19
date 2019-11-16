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

			if (enemies.Any(e => Vector3.Distance(this.transform.position, e.transform.position) <= meleDistance))
			{
				_playerController.OnStartMeleeAttack(enemies);
			}
			else
			{
				_playerController.OnStartMeleeAttack(null);
			}

			if (enemies.Any(e => Vector3.Distance(this.transform.position, e.transform.position) <= rangeDistance))
			{
				_playerController.OnStartRangeAttack(enemies);
			}
			else
			{
				_playerController.OnStartRangeAttack(null);
			}
		}
	}
}
