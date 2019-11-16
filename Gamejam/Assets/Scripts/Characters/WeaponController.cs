using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Character Character;
    public WeaponScriptableObject Weapon;
    public bool UseAllowed;

    private List<Character> targets;

    public void AddWeapon(WeaponScriptableObject weapon)
    {
        if (Weapon != null)
        {
            Weapon.DisableArtifact();
            Weapon = weapon;
        }

        else
        {
            Weapon.EnableArtifact();
        }
    }

    private void LookForTargets()
    {
        targets.Clear();

        foreach (var characters in Character.Characters)
        {
            if(characters.Key != Character.CharacterType)
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
            Weapon.currentCooldown = Weapon.Cooldown;
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
                    Weapon.currentCooldown = Weapon.Cooldown;
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
