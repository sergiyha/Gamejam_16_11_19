using System.Collections;
using System.Collections.Generic;
using FPSTestProject.Helpers.Runtime.SoundManager;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    private static int SpeedPar = Animator.StringToHash("Speed");
    
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

    protected virtual void Update()
    {
        var vel = (transform.position - lastPos) / Time.deltaTime;
        smootherVelocity = Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);
        smootherVelocity = transform.InverseTransformDirection(vel);//Vector3.Lerp(smootherVelocity, transform.InverseTransformDirection(vel), Time.smoothDeltaTime * 15f);

        animator.SetFloat(SpeedPar, 0.5f + Mathf.Clamp01(smootherVelocity.magnitude / agent.speed));

        if (Time.time - lasWalkSound > walkSoundDelay && Mathf.Clamp01(smootherVelocity.magnitude / agent.speed) > 0.5f)
        {
            SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(SoundType.Walk), transform.position, 0.5f);
            lasWalkSound = Time.time;
        }

        lastPos = transform.position;
    }
}
