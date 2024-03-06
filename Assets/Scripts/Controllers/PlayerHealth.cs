using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public int bulletDamage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletDetection>())
        {
            playerHealth -= bulletDamage;
            if (playerHealth <= 0)
            {
                //Handle death of player
            }
        }
    }
}
