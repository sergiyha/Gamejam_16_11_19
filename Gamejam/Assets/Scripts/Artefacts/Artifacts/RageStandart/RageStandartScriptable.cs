using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[CreateAssetMenu]
public class RageStandartScriptable : UsableArtifact
{
    public override bool CanPerform()
    {
        bool can = true;
        
        foreach (var target in targets)
        {
            if (target.WeaponController.LookForTargets().Count == 0)
            {
                return false;
            }
        }

        return true;
    }

    public override void Action()
    {
        base.Action();
        foreach (var target in targets)
        {
            target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));
        }
    }
}
