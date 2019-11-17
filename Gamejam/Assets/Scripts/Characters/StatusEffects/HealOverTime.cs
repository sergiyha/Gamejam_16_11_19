﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : StatusEffectBase
{
    private HealingData Data;

    public HealOverTime(BaseEffectData data, Character character) : base(data, character)
    {
        Data = (HealingData) data;

        TotalTime = Data.totalTime;
        tickTime = Data.tickTime;
        this.character = character;
    }
    public override void ApplyEffect()
    {
        character.HealthController.DoHeal(Data.healPerTick, -1);
    }
}
