using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Artifact
}

[CreateAssetMenu]
public class AxeScriptableObj : WeaponScriptableObject
{
    

    public override void Action()
    {
        foreach (var target in targets)
        {
            int dmg = Random.Range(MinDamage,MaxDamage);
            target.HealthController?.DoDamage(dmg, AnimationTime);
            if (data != null)
            {
                target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));
            }
        }
    }
}
