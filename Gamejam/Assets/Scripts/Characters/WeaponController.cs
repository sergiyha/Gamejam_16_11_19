﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public WeaponScriptableObject Weapon;
    public float atackspeedMultipl;//==1f if normal
    public bool UseAllowed;
    
    private Character character;

    private List<Character> targets;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void AddWeapon(WeaponScriptableObject weapon)
    {
        if (Weapon != null)
        {
            Weapon.DisableArtifact();
            Weapon = weapon;
            Weapon.currentCooldown = Weapon.Cooldown/atackspeedMultipl;
        }

        else
        {
            Weapon.EnableArtifact();
            StartCoroutine(Use());
        }
    }

    private void LookForTargets()
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
                || Vector3.Angle(transform.forward, (targets[i].transform.position - transform.position).normalized) <= Weapon.Angle)
            {
                targets.RemoveAt(i);
                i--;
            }
        }
    }

    public IEnumerator Use()
    {
        while (true)
        {
            if (Weapon.currentCooldown > 0f)
            {
                Weapon.currentCooldown -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                Weapon.ready = true;
                if (UseAllowed)
                {
                    Weapon.SetTargets(targets);
                    Weapon.Action();
                    character.AudioSource.PlayOneShot(Weapon.ActionSound);
                    Weapon.currentCooldown = Weapon.Cooldown/atackspeedMultipl;
                    Weapon.ready = false;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
