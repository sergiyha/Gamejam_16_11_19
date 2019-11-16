﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectBase : MonoBehaviour
{
    public float tickTime; //if tickTime =0 then sigle use while active
    public float currentTickLeft;
    public float TotalTime;//if 1000000 then aura
    public float currentTotalTime = 0f;
    public bool active;
    
    public Character character;

    public void Tick()
    {
        if (currentTickLeft > 0f)
        {
            currentTickLeft -= Time.deltaTime;
            //yield return new WaitForEndOfFrame();
        }
        else
        {
            ApplyEffect();
            currentTickLeft = tickTime;
            currentTotalTime += tickTime;
            //yield return new WaitForEndOfFrame();
        }

        
    }

    public virtual void ApplyEffect()
    {
        
    }
    
    public virtual void Remove()
    {
        
    }
}
