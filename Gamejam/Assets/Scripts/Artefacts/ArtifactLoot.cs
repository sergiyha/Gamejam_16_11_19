using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactLoot : MonoBehaviour
{
    public ArtifactBase ArtifactScriptable;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Character>() != null)
        {
            
        }
    }
}
