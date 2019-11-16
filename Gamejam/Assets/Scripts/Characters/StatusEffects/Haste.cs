using System.Collections;
using System.Collections.Generic;
using LifelongAdventure.Creatures.Data;
using UnityEngine;

public class Haste : StatusEffectBase
{
    public float speedMultiplier = 1.5f;
    public override void ApplyEffect()
    {
        character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] * speedMultiplier);
    }

    public override void Remove()
    {
        character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] / speedMultiplier);
    }
}
