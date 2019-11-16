using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsController : MonoBehaviour
{
   [SerializeField] private List<StatusEffectBase> StatusEffects;
    public StatusEffectUI UIPrefab;
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
                            effect.Remove();
                            StatusEffects.Remove(effect);
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
    }
}
