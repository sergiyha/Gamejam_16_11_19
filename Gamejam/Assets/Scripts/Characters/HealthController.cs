using System;
using System.Collections;
using System.Collections.Generic;
using LifelongAdventure.Creatures.Data;
using UnityEngine;

public class HealthController : MonoBehaviour
{

   private Character character;
   
   private void Awake()
   {
      character = GetComponent<Character>();
      
   }

   public void DoDamage(int value)
   {
      character.Stats[Stat.Health] -= value;
   }

   public void DoHeal(int value)
   {
      character.Stats[Stat.Health] += value;
   }
}
