﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactLoot : MonoBehaviour
{
    public ArtifactBase ArtifactScriptable;
    private ParticleSystem[] ps;
    public float pickupTime = 1.5f;
    private Light[] spotlight;

    private void Awake()
    {
        ps = GetComponentsInChildren<ParticleSystem>();
        spotlight = GetComponentsInChildren<Light>();
    }

    private IEnumerator ChangeIntensity()
    {
        var koef = 0f;
        var originalInt = spotlight[0].intensity;
        Debug.Log("D");
        while (koef<pickupTime*0.7f)
        {
            foreach (var light in spotlight)
            {
                light.intensity = originalInt * ((pickupTime - koef) / pickupTime);
            }
            yield return new WaitForEndOfFrame();
            koef += Time.deltaTime;
            Debug.Log(koef);
        }
        Destroy(gameObject,pickupTime/7);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Character>() != null)
        {
            Character character = other.GetComponent<Character>();
            if (character.CharacterType == Character.CharType.Player)
            {
                Debug.Log("pickup loot");
                if(SquadInventory.Instance!=null)
                    SquadInventory.Instance.AddArtefact(ArtifactScriptable);
                var main = ps[0].main;
                main.loop = false;
                StartCoroutine(ChangeIntensity());
                main.simulationSpeed *= 2f;
                var main2 = ps[1].main;
                main2.gravityModifier = new ParticleSystem.MinMaxCurve(-15f);
            }
            
        }
    }
}
