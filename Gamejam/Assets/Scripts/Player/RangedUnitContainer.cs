using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedUnitContainer : UnitContainer
{
	public RangedUnitContainer(Transform parent, List<Character> chars) : base(parent, chars)
	{
	}

	public override void Move()
	{
		for (var i = 0; i < _agents.Length; i++)
		{
			_agents[i].destination = _localTargets[i].position;
		}
	}

	public void TryAttack()
	{
		if (_enemies == null || !_enemies.Any())
		{
			if (_characters == null || !_characters.Any()) return;

			for (int i = 0; i < _characters.Count; i++)
			{
				_characters[i].AimingModule.Disable();
			}
			return;
		}

		var enemy = _enemies.First();

		if (_characters == null || !_characters.Any()) return;

		for (int i = 0; i < _characters.Count; i++)
		{
			var bow = _characters[i].WeaponController.Weapon as BowScriptableObj;
		   _characters[i].AimingModule.Aim(enemy.Character.transform, bow, _characters[i]);
		}
	}

	
	private bool IsFired; 
	

	private void Fire(int ind)
	{

	}



}
