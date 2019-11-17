using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BowScriptableObj : WeaponScriptableObject
{
    public override void Action()
    {
        foreach (var target in targets)
        {
            int dmg =Random.Range(MinDamage,MaxDamage);
            Debug.Log($"Hit {target.name} for {dmg} dmg.");
            target.HealthController.DoDamage(dmg, AnimationTime);
            if (data != null)
            {
                
                Debug.Log($"Add status effect to {target.name}");
                target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));///Если хотим стерлы с модифаером, заполняем.
            }
        }
    }
}
