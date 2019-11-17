using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedUnitContainer : UnitContainer
{
	public RangedUnitContainer(Transform parent, List<Character> chars) : base(parent, chars)
	{
	}

	public override void Move(Vector3 forward)
	{
        for (var i = 0; i < _agents.Length; i++)
        {
            _agents[i].destination = _localTargets[i].position;
            _agents[i].updateRotation = false;
            _agents[i].transform.forward = forward;
        }
    }

	public void TryAttack()
	{
		if (_enemies == null || !_enemies.Any())
		{
			for (int i = 0; i < _characters.Count; i++)
			{
				_characters[i].AimingModule.Disable();
			}
			return;
		}

		var enemy = _enemies.First();
		

		for (int i = 0; i < _characters.Count; i++)
		{
			_characters[i].AimingModule.Aim(enemy.Character.transform);
        }
    }





}
