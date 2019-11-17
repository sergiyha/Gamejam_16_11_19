using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BowScriptableObj : WeaponScriptableObject
{
    public override void Action()
    {
        int dmg = Random.Range(MinDamage, MaxDamage);
        Debug.Log($"Hit {targets[0].name} for {dmg} dmg.");
        targets[0].HealthController.DoDamage(dmg, AnimationTime);
        if (data != null)
        {
            Debug.Log($"Add status effect to {targets[0].name}");
            targets[0].StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, targets[0])); ///Если хотим стерлы с модифаером, заполняем.
        }
    }
}
