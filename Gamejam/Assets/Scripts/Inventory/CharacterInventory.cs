using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : Inventory
{
    //public int capasity;
    //public List<ArtifactBase> artifacts;
    //public List<GameObject> UIobjs;
    public WeaponScriptableObject Weapon;
    private Character character;


    private void Start()
    {
        character = GetComponent<Character>();
        if(Weapon!=null)
            character.WeaponController.AddWeapon(Weapon);
    }


    public void AddArtifact(ArtifactBase artifact)
    {
        if (artifact.ArtefactType == ItemType.Weapon)
        {
            if (Weapon != null)
            {
                //if inventory have empty slot Drop to inventory
                //else drop on ground
                Weapon = null;
                Weapon = (WeaponScriptableObject)artifact;
                character.WeaponController.AddWeapon(Weapon);
            }
            else
            {
                Weapon = (WeaponScriptableObject)artifact;
                character.WeaponController.AddWeapon(Weapon);
            }
        }

        if (artifact.ArtefactType == ItemType.Artifact)
        {
            if (artifacts.Count < capasity)
            {
                artifacts.Add(artifact);
            }
        }
    }

    public void RemoveArtifact()
    {
        
    }
}
