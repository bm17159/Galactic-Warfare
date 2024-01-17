using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebrisController : MonoBehaviour
{
    [Header("Debris Model")] 
    public GameObject debrisModel;

    
    [Header("Spawning Info")]
    [SerializeField] private int Range = 10;
    [SerializeField] private int amountToSpawn = 25;

    private Transform[] debris;
    
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
        
        
        for (int i = 0; i <= amountToSpawn; i++)
        {
            Vector3 pos = (Random.insideUnitSphere * Range) + transform.position;
            GameObject temp = Instantiate(debrisModel, pos, transform.rotation, transform);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(0, 0, 1);
            debris[i] = temp.transform;
        }
    }
}
