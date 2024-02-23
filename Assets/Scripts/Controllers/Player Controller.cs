using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

using Cursor = UnityEngine.Cursor;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float maxSpeedf;

    [SerializeField] private float brakeTime;

    [SerializeField] private float brakeSpeed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private CinemachineFreeLook fl;
    //[SerializeField] private AudioListener listener;

    //private InputManager inputManager;
    private CharacterController controller;
    public GameObject follow;
    public Rigidbody rb;
    private Vector3 rotation;
    //private Camera c;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //inputManager = InputManager.singleton;
        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, rotateSpeed);
        //c = GetComponent<Camera>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            fl.Priority = 10000;
        }
        else
        {
            fl.Priority = -1000;
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner)return;
        
        Thrust(Time.deltaTime);
        Rotate();
        rb.transform.LookAt(follow.transform);
        //transform.rotation = Quaternion.Lerp();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Thrust(float delta)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!IsOwner)return;
            rb.AddForce(follow.transform.forward * (20000 * delta));
            //rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);
        }
        else
        {
            if (!IsOwner)return;
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
        if (!IsOwner)return;
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion deltaRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion deltaRotation = Quaternion.Euler(-rotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}

