using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [Header("Spawning Info")]
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private GameObject firingPoint;

    [SerializeField] private float rps = 60;
        
    [SerializeField]
    private float fireRate;

    [SerializeField] private float timeSinceShot;
    // Start is called before the first frame update

    private bool shootDown = false;
    void Start()
    {
        fireRate = rps / 60;
        
        firingPoint = gameObject;
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
         GameObject projectile = Instantiate(projectilePrefab, firingPoint.transform.position, firingPoint.transform.localRotation);
         Rigidbody rb = projectile.GetComponent<Rigidbody>();
         //Physics.Raycast(firingPoint.transform.position, firingPoint.transform.forward, new RaycastHit(), 1.3, new int);
         //Physics.Raycast(firingPoint2.transform.position, firingPoint2.transform.forward, new RaycastHit(), 1.3, new int);

         Vector3 dir = firingPoint.transform.forward * (10000000000 * Time.deltaTime);
         rb.AddForce(dir);
         timeSinceShot = 0;
    }
    
}