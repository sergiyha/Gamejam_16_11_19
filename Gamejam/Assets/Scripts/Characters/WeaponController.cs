using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptableObject Weapon;
    public bool UseAllowed;
    public Character[] targets;

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
        targets = null;
        List<Character> targetsInAngle = new List<Character>();

        
        


        targets = targetsInAngle.ToArray();
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
