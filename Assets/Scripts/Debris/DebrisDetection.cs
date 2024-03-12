using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebrisDetection : MonoBehaviour
{
    // If a this Debris goes out of the bounds of the Spawner radius, this will handle it.
    private Rigidbody rb;
    private Vector3 currentForce;
  //  private Vector3 scale;
  //  private int scaler = 1/2;
    public Vector3 smallestSize;
    private int forceMultiplier = 100;
    public int health = 100;
    public int startingHealth = 100;
    public int bulletDamage = 50;
    public GameObject explosion;
    public Transform spawner;

    public void Init(Transform spawner)
    {
        this.spawner = spawner;
    }
    
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
            rb = gameObject.GetComponent<Rigidbody>();
            currentForce = rb.velocity;
            rb.AddForce(-currentForce*forceMultiplier);
            
        }

        if (other.GetComponent<BulletDetection>() != null)
        {
            Debug.Log("Bullet hit debris");
            Destroy(other.gameObject);
            //subtract health
            health -= bulletDamage;
            //Explode and destroy debris
            if(health <= 0)
            {
                Instantiate(explosion, transform.position, transform.localRotation);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("split");
                rb = gameObject.GetComponent<Rigidbody>();
                // scale = gameObject.transform.localScale;
                                
                rb.mass /= 2;
                
                gameObject.transform.localScale /= 2; // = new Vector3(scale.x/2, scale.y/2, scale.z/2);
                               // scale = gameObject.transform.localScale;
                forceMultiplier /= 2;
                
                Instantiate(explosion, transform.position, transform.localRotation);               
                GameObject temp = Instantiate(gameObject, spawner);
                temp.GetComponent<DebrisDetection>().Init(spawner);
                                    
                health = (1 / 2 * startingHealth);
            }

            
            /* TODO: Old code for taking multiple bullets to split
            
            // if health equals/less than 1/2 the original health, then split into 2 debris 1/2 the size.
            if (health <= 1/2 * startingHealth && health >= 0)
            {
                
            }
            
         */
                
        }
        
    }
    
    
}

