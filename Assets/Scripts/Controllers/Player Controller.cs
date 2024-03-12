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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, rotateSpeed);

        /*#region vars

        if (Class.Equals("Fighter"))
        {
            maxSpeedf = 200;
            brakeTime = 1.25f;
            brakeSpeed = -200;
            rotateSpeed = 50;
        }
        if (Class.Equals("Heavy"))
        {
            maxSpeedf = 200;
            brakeTime = 1.25f;
            brakeSpeed = -200;
            rotateSpeed = 50;
        }if (Class.Equals("Speedster"))
        {
            maxSpeedf = 200;
            brakeTime = 1.25f;
            brakeSpeed = -200;
            rotateSpeed = 50;
        }

        #endregion*/
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
        Thrust(Time.deltaTime);
        //Rotate();
        rb.transform.LookAt(follow.transform);
        //transform.rotation = Quaternion.Lerp();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    #region Thrust
    void OnThrustPerformed(InputAction.CallbackContext obj)
    {
        if (!IsOwner) return;
            if (rb.velocity.magnitude > 0f)
            {
                rb.AddForce(follow.transform.forward * (20000 * Time.deltaTime));
                //rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedf);
            }
    }
    

    private void Thrust(float delta)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            timemoved.Start();
        }
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
                if (timemoved.ElapsedMilliseconds * 100 > brakeTime)
                {
                    yield return new WaitForSeconds(brakeTime);
                }
                else
                {
                    yield return new WaitForSeconds(timemoved.ElapsedMilliseconds * 100);
                }
                timemoved.Stop();
                rb.velocity -= rb.velocity;
                
            }
            
        }

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


