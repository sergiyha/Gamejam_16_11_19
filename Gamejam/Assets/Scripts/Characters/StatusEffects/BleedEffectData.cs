using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BleedEffect", menuName = "Effects/Bleed")]
public class BleedEffectData : BaseEffectData
{
    public float tickTime;
    public float totalTime;
    
    public int damagePerTick;
}
