using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeightWhenAttack : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < 0.1f)
        {
            animator.SetLayerWeight(1, stateInfo.normalizedTime * 10);
        }
        else if (stateInfo.normalizedTime > 0.9f)
        {
            animator.SetLayerWeight(1, 1 - (stateInfo.normalizedTime - 0.9f) * 10);
        }
        else
        {
            animator.SetLayerWeight(1, 1);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 0);
    }
}
