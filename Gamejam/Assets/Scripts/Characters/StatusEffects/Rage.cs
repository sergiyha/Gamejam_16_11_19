using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : StatusEffectBase
{
    public float additionaAS = 0.2f;

    public override void ApplyEffect()
    {
        character.WeaponController.atackspeedMultipl  += additionaAS;
    }

    public override void Remove()
    {
        character.WeaponController.atackspeedMultipl  -= additionaAS;
    }
}
