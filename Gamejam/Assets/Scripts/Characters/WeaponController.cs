using System;
using System.Collections;
using System.Collections.Generic;
using FPSTestProject.Helpers.Runtime.SoundManager;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public WeaponScriptableObject Weapon;
    public float atackspeedMultipl =1f;//==1f if normal
    public bool UseAllowed;
    
    [SerializeField]
    private Character character;

    private List<Character> targets = new List<Character>();

    private float currentCooldown;

    private void Awake()
    {
        character = GetComponent<Character>();
       //AddWeapon(Weapon);
    }


    public void AddWeapon(WeaponScriptableObject weapon)
    {
        if (Weapon != null)
        {
            Weapon.DisableArtifact();
        }

        Weapon = weapon;
        currentCooldown = Weapon.Cooldown / atackspeedMultipl;
        character.AnimationController.SetController(Weapon.aoc);

        StopAllCoroutines();
        StartCoroutine(Use());
    }

    public List<Character> LookForTargets()
    {
        targets.Clear();

        foreach (var characters in Character.Characters)
        {
            if(characters.Key != character.CharacterType)
                targets.AddRange(characters.Value);
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(targets[i].transform.position, transform.position) > Weapon.Range
             /*   || Vector3.Angle(transform.forward, (targets[i].transform.position - transform.position).normalized) > Weapon.Angle*/)
            {
                targets.RemoveAt(i);
                i--;
            }
        }

        return targets;
    }

    public IEnumerator Use()
    {
        while (true)
        {
            //Debug.Log(Weapon.currentCooldown);
            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                if (UseAllowed)
                {
                    LookForTargets();
                    if (targets.Count > 0)
                    {
                        //Weapon.useAllowed = UseAllowed;

                        Weapon.SetTargets(targets);

                        SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(Weapon.ActionSound), transform.position);
                        Weapon.Action();
                        character.AnimationController.DoAttack();

                        currentCooldown = Weapon.Cooldown / atackspeedMultipl;

                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            yield return new WaitForEndOfFrame();;
        }

        yield return null;
    }
}
