using System.Collections;
using System.Collections.Generic;
using FPSTestProject.Helpers.Runtime.SoundManager;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    private static int SpeedPar = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("IsDead");

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;


    [SerializeField]
    private float walkSoundDelay = 1;

    public Animator Animator => animator;

    protected Vector3 smootherVelocity;
    protected Vector3 lastPos;

    private float lasWalkSound;

    public void DoAttack()
    {
        animator.SetTrigger(Attack);
    }

    public void DoDeath()
    {
        animator.SetTrigger(Death);
    }

    protected virtual void Update()
    {
        var vel = (transform.position - lastPos) / Time.deltaTime;
        smootherVelocity = Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);
        smootherVelocity = transform.InverseTransformDirection(vel);//Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);

        var current = Mathf.Clamp01(smootherVelocity.magnitude / agent.speed);
        animator.SetFloat(SpeedPar, 0.5f + current);

        if (Time.time - lasWalkSound > walkSoundDelay && current > 0.5f)
        {
            SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(SoundType.Walk), transform.position, 0.5f);
            lasWalkSound = Time.time;
        }

        lastPos = transform.position;
    }

    public void SetController(AnimatorOverrideController weaponAoc)
    {
        Animator.runtimeAnimatorController = weaponAoc;
    }
}
