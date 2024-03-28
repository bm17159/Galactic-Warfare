using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ShootingController : MonoBehaviour
{
    [Header("Spawning Info")]
    [SerializeField]
    private GameObject projectilePrefab;

    [Header("Firing Points")]
    [SerializeField] private Transform[] firingPoints;

    [SerializeField] private Transform firingPoint1;
    
    

    [SerializeField] private float rps = 60;
        
    [SerializeField]
    private float fireRate;

    [SerializeField] private float timeSinceShot;
    // Start is called before the first frame update

    private bool shootDown = false;
    void Start()
    {
        fireRate = rps / 60;
        
        InputManager.singleton.shoot.performed += OnShootPerformed;
        InputManager.singleton.shoot.canceled += OnShootCanceled;
    }
    
    // Update is called once per frame
    private void Update()
    {
        // increase the time since last shot
        timeSinceShot += Time.deltaTime;
        // if the fire button is down and enough time has passed, shoot
        if (shootDown && timeSinceShot >= fireRate)
        {
            Shoot();
        }
        
    }
    


    private void OnShootCanceled(InputAction.CallbackContext obj)
    {
        shootDown = false;
    }

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        shootDown = true;
    }

    private void Shoot()
    {
        for (int i = 0; i < firingPoints.Length; i++)
        {
            if (firingPoints[i] != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firingPoints[i].position, firingPoints[i].localRotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
         
                Vector3 dir = firingPoints[i].forward * (1000000 * Time.deltaTime);
                rb.AddForce(dir);
                timeSinceShot = 0;
            }
            
        }
        /*GameObject projectile = Instantiate(projectilePrefab, firingPoint1.position, firingPoint1.localRotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
         
        Vector3 dir = firingPoint1.transform.forward * (100000 * Time.deltaTime);
        rb.AddForce(dir);
        timeSinceShot = 0;*/
    }
    
}