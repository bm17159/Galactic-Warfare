using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class DebrisController : MonoBehaviour
{
    [Header("Models")] public GameObject debrisModel1;
    public GameObject debrisModel2;
    public GameObject debrisModel3;
    public GameObject debrisModel4;
    public GameObject debrisModel5;
    [SerializeField] private int modelCount = 5;
    private GameObject[] debrisModels;

    
    [Header("Spawning Info")] [SerializeField]
    private int Range = 100;
    [SerializeField] private int debrisAmount = 50;
    public Transform[] debris;


    [Header("Scaling Info")] [SerializeField]
    private float minScale = 0.1f;
    [SerializeField] private float maxScale = 2f;
    
    
    [Header("Force Applied")] [SerializeField]
    private int forceMultiplier = 10;


    // Start is called before the first frame update
    void Start()
    {
        debris = new Transform[debrisAmount];
        initializeAsteriods();
        CreateDebris();

    
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void initializeAsteriods()
    {
        debrisModels = new GameObject[modelCount];
        debrisModels[0] = debrisModel1;
        debrisModels[1] = debrisModel2;
        debrisModels[2] = debrisModel3;
        debrisModels[3] = debrisModel4;
        debrisModels[4] = debrisModel5;


    }
    private void CreateDebris()
    {
        // Create and position 25 Debris in a radius
        for (int i = 0; i < debrisAmount; i++)
        {

            // Calculate Position of Next Debris and Force
            Vector3 pos = (Random.insideUnitSphere * Range) + transform.position;
            Vector3 force = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f),
                Random.Range(-10.0f, 10.0f));
            
            // Decide what model to use
            int x = Random.Range(0, debrisModels.Length);

            // Spawn the new Debris with a random size
            float ranScale = Random.Range(minScale, maxScale);
            Vector3 scale = new Vector3(ranScale, ranScale, ranScale);
            GameObject temp = Instantiate(debrisModels[x], pos, Random.rotation, transform);
            temp.transform.localScale = scale;
            // Add a random force to each Debris
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(force * forceMultiplier);
            rb.AddTorque(force * forceMultiplier);
            // Store in Array for future
            debris[i] = temp.transform;
        }
    }

   
}
    


    
