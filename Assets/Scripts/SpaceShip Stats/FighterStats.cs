using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShipStats : ScriptableObject
{
   
   [Header("Ship stats")]
   public float TopSpeed = 100;
   public float Acceleration = 25;
   public float BreakingSpeed = 18;
   public float Size = 100;
   public float Health = 125;
   public float damageReductionDebris = 25;
   
   [Header("Ability")]
   public float abilityCooldown = 15;
   public float abilityDamage = 25;
   public float abilityRange = 25;
   public float slowDuration = 5;
   
   [Header("Gun Stats")]
   public float gunDamage = 20;
   public float FireRate = 5;
   public float Range = 25;

}
