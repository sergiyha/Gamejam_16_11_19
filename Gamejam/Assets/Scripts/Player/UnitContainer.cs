﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitContainer 
{
	public void AddTargets(List<Transform> targets)
	{
		_localTargets = targets;
	}

	protected List<CharacterAndDistance> _enemies;
	protected List<Character> _characters;

	public void UpdateChars(List<Character> chars)
	{
        foreach (Character character in chars)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
                character.OnDead += CharacterOnOnDead;
            }
        }

    }

	public bool Contains(NavMeshAgent charNavMesh, Character character)
	{
		return _agents.Contains(charNavMesh) || _characters.Contains(character);
	}

	private void CharacterOnOnDead(Character character)
    {
        _characters.Remove(character);
        _agents = _characters.Select(c => c.GetComponent<NavMeshAgent>()).ToList();
    }

	public void AddAgentAndCharacter(NavMeshAgent charNavMesh, Character character)
	{
		_agents.Add(charNavMesh);
		_characters.Add(character);
	}

	public void RemoveCharacterAndAgent(NavMeshAgent charNavMesh, Character character)
	{
		_agents.Remove(charNavMesh);
		_characters.Remove(character);
	}

	public UnitContainer(Transform parent, List<Character> characters )
	{
		_parent = parent;
		_characters = characters;

        foreach (var character in _characters)
        {
            character.OnDead += CharacterOnOnDead;
        }
    }

	private readonly Transform _parent;

	public void SetEnemies(List<CharacterAndDistance> enemiesData)
	{
		_enemies = enemiesData;
	}

	protected List<Transform>_localTargets;
	protected List<NavMeshAgent>_agents;

	public List<NavMeshAgent> GetAgents()
	{
		return _agents;
	}

	public void AddAgents(List<NavMeshAgent> agents)
	{
		_agents = agents;
	}

	

	public virtual void Move(Vector3 forward)
	{
		if (_enemies != null && _enemies.Any())
		{
			var minDistCharacter = _enemies.OrderBy(e => e.Distance).First();


			foreach (var navMeshAgent in _agents)
			{
                if (minDistCharacter.Character)
                {
                    navMeshAgent.updateRotation = true;
                    navMeshAgent.destination = minDistCharacter.Character.transform.position;
                }
            }
			return;
		}

		//Local targets
		for (var i = 0; i < _agents.Count; i++)
        {
            _agents[i].updateRotation = false;
            _agents[i].transform.forward = forward;
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
