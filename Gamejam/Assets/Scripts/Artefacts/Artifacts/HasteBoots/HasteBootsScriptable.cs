using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HasteBootsScriptable : UsableArtifact
{
    public override bool CanPerform()
    {
        return true;
    }

    public override void Action()
    {
        Debug.Log("Use haste boots");
        base.Action();
        foreach (var target in targets)
        {
            target.StatusEffectsController.AddStatusEffect(BaseEffectData.Create(data, target));
        }
    }
}
