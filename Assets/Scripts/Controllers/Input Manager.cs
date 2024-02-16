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
    }

    // Start is called before the first frame update
    void Start()
    {
        shoot = controls.Locomotion.Shoot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }
}