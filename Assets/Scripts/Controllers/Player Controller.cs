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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //inputManager = InputManager.singleton;
        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, rotateSpeed);
        c = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Thrust(Time.deltaTime);
        Rotate();
        //rb.transform.LookAt(follow.transform);
        //transform.rotation = Quaternion.Lerp();
    }

    private void Thrust(float delta)
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(follow.transform.forward * (20000 * delta));
            //rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);
        }
        else
        {
            
            if (rb.velocity.magnitude > 0f)
            {
                StartCoroutine(Slow());
            }

            IEnumerator Slow()
            {
                rb.AddForce(follow.transform.forward * (brakeSpeed * delta));
                yield return new WaitForSeconds(brakeTime);
                rb.velocity -= rb.velocity;
                
            }
            
        }

    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion deltaRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            c.MoveRotation(rb.rotation * deltaRotation);
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion deltaRotation = Quaternion.Euler(-rotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            c.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}

