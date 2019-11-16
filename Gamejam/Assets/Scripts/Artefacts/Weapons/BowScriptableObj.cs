using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BowScriptableObj : WeaponScriptableObject
{
    public override void Action()
    {
        base.Action();
        foreach (var target in targets)
        {
            if (data != null)
            {
                Debug.Log($"Add status effect to {target.name}");
                target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));
            }
        }
    }
}
