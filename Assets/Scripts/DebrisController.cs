using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebrisController : MonoBehaviour
{
    [Header("Debris Model")] public GameObject debrisModel;


    [Header("Spawning Info")] [SerializeField]
    private int Range = 10;

    [SerializeField] private int amountToSpawn = 25;
    private Transform[] debris;

    [Header("Force Applied")] [SerializeField]
    private int forceMultiplier = 10;


    // Start is called before the first frame update
    void Start()
    {
        debris = new Transform[amountToSpawn];
        CreateDebris();

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void CreateDebris()
    {

        // Create and position 25 Debris in a radius
        for (int i = 0; i < amountToSpawn; i++)
        {
            // Calculate Position of Next Debris and Force
            Vector3 pos = (Random.insideUnitSphere * Range) + transform.position;
            Vector3 force = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f),
                Random.Range(-10.0f, 10.0f));
            // Spawn the new Debris
            GameObject temp = Instantiate(debrisModel, pos, transform.rotation, transform);
            // Add a random force to each Debris
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(force * forceMultiplier);
            // Store in Array
            debris[i] = temp.transform;
        }
    }

}
    
