using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager singleton;

    public Controls controls;

    public InputAction shoot;

    public InputAction thrust;

    public InputAction rotate;
    
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
        
        controls = new Controls();
        controls.Enable();
        thrust = controls.Locomotion.Thrust;
        rotate = controls.Locomotion.Rotation;
        shoot = controls.Locomotion.Shoot;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }
}