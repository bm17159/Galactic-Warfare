using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    [Header("Debris Model")]
    public GameObject Debris;

    [Header("Debris Spawner (At Origin)")]
    [SerializeField] private Transform SpawnerPos;
    [SerializeField] private int Range = 10;
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnerPos = this.transform;
        transform.position = Random.insideUnitSphere * Range;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
