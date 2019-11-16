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

    [SerializeField]
    protected Vector3 smootherVelocity;
    protected Vector3 lastPos;


    protected virtual void Update()
    {
        var vel = (transform.position - lastPos) / Time.deltaTime;
        smootherVelocity = Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);
        smootherVelocity = transform.InverseTransformDirection(vel);//Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);

        animator.SetFloat(SpeedPar, 0.5f + Mathf.Clamp01(smootherVelocity.magnitude / agent.speed));

        lastPos = transform.position;
    }
}
