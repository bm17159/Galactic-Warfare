using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AIControler : MonoBehaviour
{

   
    public GameObject target;
    public float Speed;
    public float distanceFromEnemy;
    private bool ismoving;
    private Rigidbody rb;

   



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
                    transform.LookAt(target.transform);
        
        

    }
}
