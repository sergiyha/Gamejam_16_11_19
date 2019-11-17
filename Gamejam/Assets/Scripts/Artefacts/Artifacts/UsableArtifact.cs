using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    itself,
    ally,
    enemy,
    point
}
public class UsableArtifact : ArtifactBase
{
    public float Range;
    public int Angle;
    public float Cooldown;
    public float animationTime;
    public AudioClip ActionSound;
    public AudioClip ImpactSound;
    
    public bool useAllowed;
    public bool ready = false;
    public float currentCooldown = 0f;
    public BaseEffectData data;
    public Vector3 point;
    public float effectRadius;
    public TargetType TargetType;
    public virtual  bool CanPerform()
    {
        return true;
    }
    public void SetTargets(List<Character> targ)
    {
        targets = targ;
    }
}
