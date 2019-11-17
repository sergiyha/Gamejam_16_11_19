using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffect", menuName = "Effects/Slow")]
public class SlowEffectData : BaseEffectData
{
    public float tickTime;
    public float totalTime;
    public float speedMultiplier = 0.7f;
    
}

