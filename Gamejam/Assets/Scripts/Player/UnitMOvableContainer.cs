using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovableContainer
{
	public void AddTargets(Transform[] targets)
	{
		_localTargets = targets;
	}

	private List<CharacterAndDistance> _enemies;

	public UnitMovableContainer(Transform parent)
	{
		_parent = parent;
	}

	private readonly Transform _parent;

	public void SetEnemies(List<CharacterAndDistance> enemiesData)
	{
		_enemies = enemiesData;
	}

	private Transform[] _localTargets;
	private NavMeshAgent[] _agents;

	public void AddAgents(NavMeshAgent[] agents)
	{
		_agents = agents;
	}

	public void Move()
	{
		if (_enemies != null && _enemies.Any())
		{
			var minDistCharacter = _enemies.OrderBy(e => e.Distance).First();


			foreach (var navMeshAgent in _agents)
			{
				if (minDistCharacter.Character)
					navMeshAgent.destination = minDistCharacter.Character.transform.position;
			}
			return;
		}

		for (var i = 0; i < _agents.Length; i++)
		{
			_agents[i].destination = _localTargets[i].position;
		}
	}


	public void Unattach()
	{
		foreach (var navMeshAgent in _agents)
		{
			if (navMeshAgent.transform.parent != null)
				navMeshAgent.transform.parent = null;
		}
	}

	public void Attach()
	{

		foreach (var navMeshAgent in _agents)
		{
			if (navMeshAgent.transform.parent != _parent)
				navMeshAgent.transform.parent = _parent;
		}
	}
}

public class CharacterAndDistance
{
	public float Distance;
	public Character Character;
}
