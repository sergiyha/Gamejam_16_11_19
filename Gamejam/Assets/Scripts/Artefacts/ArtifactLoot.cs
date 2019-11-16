using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactLoot : MonoBehaviour
{
    public ArtifactBase ArtifactScriptable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();
            if (character.CharacterType == Character.CharType.Player)
            {
                SquadInventory.Instance.AddArtefact(ArtifactScriptable);
                Destroy(gameObject);
            }
        }
    }
}
