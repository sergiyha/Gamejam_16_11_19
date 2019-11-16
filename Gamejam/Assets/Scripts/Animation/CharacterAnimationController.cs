using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    private static int SpeedPar = Animator.StringToHash("Speed");
    
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;

    public Animator Animator => animator;

    protected Vector3 smootherVelocity;


    protected virtual void Update()
    {
        smootherVelocity = Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(agent.velocity), Time.smoothDeltaTime * 15f);

        animator.SetFloat(SpeedPar, 0.5f + Mathf.Clamp01(smootherVelocity.x / agent.speed));
    }
}
