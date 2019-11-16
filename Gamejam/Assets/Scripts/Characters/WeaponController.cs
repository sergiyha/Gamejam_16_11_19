using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Character character;
    public WeaponScriptableObject Weapon;
    public float atackspeedMultipl;//==1f if normal
    public bool UseAllowed;
    public Character[] targets;

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
        targets = null;
        List<Character> targetsInAngle = new List<Character>();

        
        


        targets = targetsInAngle.ToArray();
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
