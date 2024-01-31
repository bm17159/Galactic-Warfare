using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : ScriptableObject
{
    
    [Header("Ship stats")]
    public float topSpeed = 80;
    public float Accerleration = 22;
    public float BreakingSpeed = 10;
    public float Size = 150;
    public float Health = 175;
    public float damageReductionDebris = 50;

    [Header("Gun Stats")]
    public float gunDamage = 25;
    public float fireRate = 2;
    public float Range = 25;
    
    [Header("Ability")]
    public float abilityCooldown = 25;
    public float abilityDuration = 75;
    
    
    
}
