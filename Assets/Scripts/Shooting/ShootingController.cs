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
    [SerializeField]
    private GameObject firingPoint;

    [SerializeField] private float rpm = 60;
        
    [SerializeField]
    private float fireRate;

    [SerializeField] private float timeSinceShot = 0;
    // Start is called before the first frame update

    private bool shootDown = false;
    void Start()
    {
        fireRate = rpm / 60;
        
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
         GameObject projectile = Instantiate(projectilePrefab, firingPoint.transform.position, firingPoint.transform.rotation);
         Rigidbody rb = projectile.GetComponent<Rigidbody>();
         rb.AddForce(0, 0, 1000);
         timeSinceShot = 0;
    }
    
}
