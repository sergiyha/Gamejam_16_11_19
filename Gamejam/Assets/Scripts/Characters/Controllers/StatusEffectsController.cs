using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectsController : MonoBehaviour
{
    [SerializeField]
    private Transform StatusEffectsGrid;

    [SerializeField]
    private List<StatusEffectBase> StatusEffects;

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private IEnumerator StatusUpdate()
    {
        while (true)
        {
            if (StatusEffects.Count > 0)
            {
                foreach (var effect in StatusEffects)
                {
                    if (!effect.active)
                    {
                        if (effect.currentTotalTime < effect.TotalTime)
                        {
                            effect.Tick();
                        }
                        else
                        {
                            RemoveStatusEffect(effect);
                        }
                    }
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void AddStatusEffect(StatusEffectBase statusEffect)
    {
        StatusEffects.Add(statusEffect);
        statusEffect.character = character;

        var prototype = StatusEffectsGrid.GetChild(0);
        var effectHolder = Instantiate(prototype.gameObject, StatusEffectsGrid);
        effectHolder.GetComponent<StatusEffectUI>().SetUp(statusEffect, false);
        effectHolder.name = statusEffect.InstanceId.ToString();
        effectHolder.SetActive(true);
    }

    private void RemoveStatusEffect(StatusEffectBase statusEffect)
    {
        statusEffect.Remove();
        StatusEffects.Remove(statusEffect);

        var childCount = StatusEffectsGrid.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (StatusEffectsGrid.GetChild(i).name.Equals(statusEffect.InstanceId.ToString()))
            {
                Destroy(StatusEffectsGrid.GetChild(i));
                return;
            }
        }
    }
}
