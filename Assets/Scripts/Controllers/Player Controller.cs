using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

using Cursor = UnityEngine.Cursor;
using Debug = UnityEngine.Debug;

public class PlayerController : NetworkBehaviour
{
    #region Vars
    [SerializeField] private float maxSpeedf;

    [SerializeField] private float brakeTime;

    [SerializeField] private float brakeSpeed;

    [SerializeField] private float rotateSpeed;
    
    [SerializeField] private string Class;

    [SerializeField] private CinemachineFreeLook fl;
    //[SerializeField] private AudioListener listener;

    private Stopwatch timemoved;

    private InputManager inputManager;
    private CharacterController controller;
    public GameObject follow;
    public Rigidbody rb;
    private Vector3 rotation;

    private bool thrustDown = false;
    private bool rotateDown = false;
    
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, rotateSpeed);

        InputManager.singleton.thrust.performed += OnThrustPerformed;
        InputManager.singleton.thrust.canceled += OnThrustCanceled;
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
        if (!IsOwner) return;
        if (thrustDown)
        {
            Thrust(Time.deltaTime);
        }
        rb.transform.LookAt(follow.transform);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    #region Thrust
    private void OnThrustPerformed(InputAction.CallbackContext obj)
    {
        thrustDown = true;
        rb.AddForce(follow.transform.forward * (20000 * Time.deltaTime));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);
    }
    private void OnThrustCanceled(InputAction.CallbackContext obj)
    {
        thrustDown = false;
        if (rb.velocity.magnitude > 0f)
        {
            StartCoroutine(Slow());
        }
        IEnumerator Slow()
        {
            rb.AddForce(follow.transform.forward * (brakeSpeed * Time.deltaTime));
            yield return new WaitForSeconds(brakeTime);
            rb.velocity -= rb.velocity;
        }
        
    }

    private void Thrust(float delta)
    {
        rb.AddForce(follow.transform.forward * (20000 * delta));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);
    }

    #endregion

            /*#region Rotate

            void OnRotateCanceled(InputAction.CallbackContext obj)
            {
                rotateDown = false;
            }

            void OnRotatePerformed(InputAction.CallbackContext obj)
            {
                Debug.Log("rotate");
                rotateDown = true;
            }

            void Rotate()
            {
                if (!IsOwner) return;
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

            #endregion*/
        }


