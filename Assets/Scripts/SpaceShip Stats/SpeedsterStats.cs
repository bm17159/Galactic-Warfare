using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterStats : ScriptableObject
{
  
   [Header("ship Stats")]
   public float topSpeed = 170;
   public float Acceleration = 45;
   public float BreakingSpeed = 25;
   public float Size = 60;
   public float Health = 75;
   public float damageReductionDebris = 13;

   [Header("Gun stats")]
   public float gunDamage = 15;
   public float fireRate = 10;
   public float Range = 25;
   
   [Header("Ability stats")] 
   public float abilityCooldown = 10;
   public float abilityDuration = 15;




}
