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
            Debug.Log($"Hit {target.name} for {dmg} dmg.");
            target.HealthController.DoDamage(dmg);
            if (data != null)
            {
                Debug.Log($"Add bleed to {target.name}");
                target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));
            }
        }
    }
}
