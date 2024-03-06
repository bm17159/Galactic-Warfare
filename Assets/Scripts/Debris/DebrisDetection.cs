using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebrisDetection : MonoBehaviour
{
    // If a this Debris goes out of the bounds of the Spawner radius, this will handle it.
    private Rigidbody rb;
    private Vector3 currentForce;
    private Vector3 scale;
    private int scaler = 1/2;
    public Vector3 smallestSize;
    private int forceMultiplier = 100;
    public int health = 100;
    public int startingHealth = 100;
    public int bulletDamage = 50;
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DebrisController>() != null)
        {
            // Reverse velocity when leaves the area
            //Debug.Log(this.name + " has left the sphere");
            rb = this.GetComponent<Rigidbody>();
            currentForce = rb.velocity;
            rb.AddForce(-currentForce*forceMultiplier);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            // destroy or move this rock
            //Debug.Log("Debris hit Ship");
            rb = gameObject.GetComponent<Rigidbody>();
            currentForce = rb.velocity;
            rb.AddForce(-currentForce*forceMultiplier);
            
        }

        if (other.GetComponent<BulletDetection>() != null)
        {
            Debug.Log("Bullet hit debris");
            //subtract health
            health -= bulletDamage;
            // if health equals/less than 1/2 the original health, then split into 2 debris 1/2 the size.
            if (health <= 1/2 * startingHealth && health >= 0)
            {
                Debug.Log("split");
                rb = gameObject.GetComponent<Rigidbody>();
                scale = gameObject.transform.localScale;
                gameObject.transform.localScale.Set((scale.x * scaler),(scale.y * scaler), (scale.z * scaler));
                rb.mass *= 1/2;
                //if the scale is smaller than the smallest allowed mass, then destroy the object instead of spliting object.
                if (rb.mass <= 0.25)
                {
                    Debug.Log("split");
                    gameObject.transform.localScale.Set(scale.x/2, scale.y/2, scale.z/2);
                    scale = gameObject.transform.localScale;
                    Instantiate(gameObject);
                    
                    health = (1 / 2 * startingHealth);
                }
                else if(health <= 0 || rb.mass <= 0)
                {
                    Destroy(gameObject);
                    
                } 
                
            }
            


        }
    }
}
