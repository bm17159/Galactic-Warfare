using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHealth : MonoBehaviour
{
    // If a this Debris goes out of the bounds of the Spawner radius, this will handle it.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DebrisController>() != null)
        {
            // destroy or move this rock
            Destroy(gameObject);
            
        }
    }
}
