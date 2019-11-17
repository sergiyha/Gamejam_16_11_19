using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : StatusEffectBase
{
    private HealingEffectData Data;

    public Healing(BaseEffectData data, Character character) : base(data, character)
    {
        Data = (HealingEffectData) data;

        TotalTime = Data.totalTime;
        tickTime = Data.tickTime;
        this.character = character;
    }
    public override void ApplyEffect()
    {
        character.HealthController.DoHeal(Data.healPerTick, -1);
        character.StatusEffectsController.healingFX.SetActive(true);
    }public override void Remove()
    {
        character.StatusEffectsController.healingFX.SetActive(false);
    }
}
