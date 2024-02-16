using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    // If a this Debris goes out of the bounds of the Spawner radius, this will handle it.
    private Rigidbody rb;
    private Vector3 currentForce;
    private int forceMultiplier = 100;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DebrisController>() != null)
        {
            // Reverse velocity when leaves the area
            Debug.Log(this.name + " has left the sphere");
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
            Destroy(gameObject);
        }
    }
}
