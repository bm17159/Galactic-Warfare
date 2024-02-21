using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetection : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DebrisController>() != null)
        {
            Debug.Log("Bullet left the sphere");
           Destroy(gameObject);
        }
    }

}
