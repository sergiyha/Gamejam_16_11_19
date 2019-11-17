using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitContainer 
{
	public void AddTargets(Transform[] targets)
	{
		_localTargets = targets;
	}

	protected List<CharacterAndDistance> _enemies;
	protected List<Character> _characters;

	public void UpdateChars(List<Character> chars)
	{
		_characters = chars;
	}

	public UnitContainer(Transform parent, List<Character> characters )
	{
		_parent = parent;
		_characters = characters;
	}

	private readonly Transform _parent;

	public void SetEnemies(List<CharacterAndDistance> enemiesData)
	{
		_enemies = enemiesData;
	}

	protected Transform[] _localTargets;
	protected NavMeshAgent[] _agents;

	public NavMeshAgent[] GetAgents()
	{
		return _agents;
	}

	public void AddAgents(NavMeshAgent[] agents)
	{
		_agents = agents;
	}

	public virtual void Move()
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
