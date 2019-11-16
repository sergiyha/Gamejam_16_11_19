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
        base.Action();
        foreach (var target in targets)
        {
            target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data,target));
        }
    }
}
