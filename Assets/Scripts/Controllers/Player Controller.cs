using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeedf;

    [SerializeField] private float brakeTime;

    [SerializeField] private float brakeSpeed;

    [SerializeField] private float rotateSpeed;

    //private InputManager inputManager;
    private CharacterController controller;
    public GameObject follow;
    public Rigidbody rb;
    private Vector3 rotation;
    public Rigidbody c;

    private bool thrustDown = false;
    private bool rotateDown = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, rotateSpeed);
        
        
        
        InputManager.singleton.thrust.performed += OnThrustPerformed;
        InputManager.singleton.thrust.canceled += OnThrustCanceled;
        InputManager.singleton.rotate.performed += OnRotatePerformed;
        InputManager.singleton.rotate.canceled += OnRotateCanceled;
    }

    void Update()
    {
        if (thrustDown)
        {
            Thrust();
        }

        if (rotateDown)
        {
            Rotate();
        }
        rb.transform.LookAt(follow.transform);
        //transform.rotation = Quaternion.Lerp();
    }
    #region Thrust 
    private void OnThrustCanceled(InputAction.CallbackContext obj)
    {
        
        if (rb.velocity.magnitude > 0f)
        {
            StartCoroutine(Slow());
        }

        IEnumerator Slow()
        {
            rb.AddForce(follow.transform.forward * (-brakeSpeed * Time.deltaTime));
            yield return new WaitForSeconds(brakeTime);
            rb.velocity -= rb.velocity;
                
        }
        thrustDown = false;
    }

    private void OnThrustPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("forward");
        thrustDown = true;
    }
    
   
    private void Thrust()
    {
       
            rb.AddForce(follow.transform.forward * (20000 * Time.deltaTime));
            //rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);

           
            
        }
    #endregion

    #region Rotate
    private void OnRotateCanceled(InputAction.CallbackContext obj)
    {
        rotateDown = false;
    }

    private void OnRotatePerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("rotate");
        rotateDown = true;
    }

    private void Rotate()
    {
       
        
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        c.MoveRotation(rb.rotation * deltaRotation);
            
        
        
        /*Quaternion deltaRotation = Quaternion.Euler(-rotation * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        c.MoveRotation(rb.rotation * deltaRotation);*/
        
    } 
    

    #endregion
   
}

