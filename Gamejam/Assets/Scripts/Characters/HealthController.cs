using System;
using System.Collections;
using System.Collections.Generic;
using FPSTestProject.Helpers.Runtime.SoundManager;
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
		if(character.Stats[Stat.Health]>0)
            return;
        character.DoDeath();
    }


    public void DoDamage(int value, float delay)
    {
        delay = -1;
        if (delay <= 0)
        {
            if (character.Stats[Stat.Health] > 0)
            {
                character.Stats[Stat.Health] -= value;
                UpdateHealth();
                SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(SoundType.TakeDamage), transform.position, 1, transform);
                CheckDead();
            }
        }
        else
            StartCoroutine(ApplyDamage(value, delay));
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

    private IEnumerator ApplyDamage(int value, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (character.Stats[Stat.Health] > 0)
        {
            character.Stats[Stat.Health] -= value;
            UpdateHealth();
            SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(SoundType.TakeDamage), transform.position, 1, transform);
            CheckDead();
        }
    }

}
