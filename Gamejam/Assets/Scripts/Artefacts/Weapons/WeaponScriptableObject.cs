using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon")]
public class WeaponScriptableObject : ArtifactBase
{
    public int MinDamage;
    public int MaxDamage;
    public float Range;
    public float Cooldown;
    public float AnimationTime;
    public int Angle;
    
    public AudioClip ActionSound;
    public AudioClip ImpactSound;
    
    public AnimatorOverrideController aoc;
    
    public bool useAllowed;
    
    
    public bool ready = false;
    public float currentCooldown = 0f;
    public BaseEffectData data;
    
    

    public override void Action()
    {
       
    }

    public void SetTargets(List<Character> targ)
    {
        targets = targ;
    }

//    public void DisableArtifact()
//    {
//        
//    }
//
//    public void EnableArtifact()
//    {
//    }
}
