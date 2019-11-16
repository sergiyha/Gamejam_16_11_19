using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffectBase
{
    public int damagePerTick;
    public override void ApplyEffect()
    {
        character.HealthController.DoDamage(damagePerTick);
    }
}
