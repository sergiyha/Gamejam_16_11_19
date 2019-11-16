using System;
using System.Collections;
using System.Collections.Generic;
using LifelongAdventure.Creatures.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        UpdateHealth();
    }

	private void CheckDead()
	{
		if(character.Stats[Stat.Health]<=0)Destroy(this.gameObject);
	}


	public void DoDamage(int value)
    {
        character.Stats[Stat.Health] -= value;
        UpdateHealth();
	    CheckDead();


    }

    public void DoHeal(int value)
    {
        character.Stats[Stat.Health] += value;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        character.UiController.UpdateHealth(character.Stats[Stat.Health], character.InitialStats[Stat.Health]);
    }

    [Button]
    public void Test()
    {
        DoDamage(3);
    }
}
