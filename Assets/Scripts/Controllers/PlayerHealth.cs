using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public int bulletDamage = 10;
    public int debrisDamage = 50;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (other.GetComponent<BulletDetection>())
        {
            playerHealth -= bulletDamage;
            if (playerHealth <= 0)
            {
                //Handle death of player
            }
        }
        if (other.GetComponent<DebrisDetection>())
        {
            playerHealth -= debrisDamage;
            rb.velocity = new Vector3(0,0,0);
            
            if (playerHealth <= 0)
            {
                //Handle death of player
            }
        }
    }
}
