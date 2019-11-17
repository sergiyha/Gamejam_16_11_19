using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectsController : MonoBehaviour
{
    public GameObject hasteFX;
    public GameObject rageFx;
    [SerializeField]
    private List<StatusEffectBase> StatusEffects = new List<StatusEffectBase>();

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        StartCoroutine(StatusUpdate());
    }

    private IEnumerator StatusUpdate()
    {
        while (true)
        {
            if (StatusEffects.Count > 0)
            {
                for (var index = 0; index < StatusEffects.Count; index++)
                {
                    var effect = StatusEffects[index];
                    if (!effect.active)
                    {
                        Debug.Log(effect.InstanceId + " - " + effect.currentTotalTime + " - " + effect.TotalTime);
                        if (effect.currentTotalTime < effect.TotalTime)
                        {
                            effect.Tick();
                        }
                        else
                        {
                            RemoveStatusEffect(effect);
                            index--;
                        }
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void AddStatusEffect(StatusEffectBase statusEffect)
    {
        StatusEffects.Add(statusEffect);
        statusEffect.character = character;

        character.UiController.AddStatusEffect(statusEffect);
    }

    private void RemoveStatusEffect(StatusEffectBase statusEffect)
    {
        statusEffect.Remove();
        StatusEffects.Remove(statusEffect);

        character.UiController.RemoveStatusEffect(statusEffect);
    }
}
