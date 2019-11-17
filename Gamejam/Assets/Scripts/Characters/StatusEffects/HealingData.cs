using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealingEffect", menuName = "Effects/Healing")]
public class HealingData : BaseEffectData
{
    public float tickTime;
    public float totalTime;
    
    public int healPerTick;
}
