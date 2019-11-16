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

	private List<Character> _enemies;
	public void SetEnemies(List<Character> enemies)
	{
		_enemies = enemies;
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
			for (var i = 0; i < _agents.Length; i++)
			{
				_agents[i].destination = _enemies[0].transform.position;
			}
			return;
		}

		for (var i = 0; i < _agents.Length; i++)
			{
				_agents[i].destination = _localTargets[i].position;
			}
	}
}
