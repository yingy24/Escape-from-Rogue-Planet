﻿using UnityEngine;
using System.Collections;

public class UpdateCharacterController : MonoBehaviour
{
    public GameObject camera;
    public GameManager gameManager;

    // === This code is learned from RenaisanceCoder ===

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGrounded = 0.1f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput, jumpInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
    }

    void Start()
    {
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();

        forwardInput = turnInput = jumpInput = 0;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS); //Interpolated
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS); //Interpolated
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS); //Non-interpolated
    }

    // Update is called once per frame
    void Update()
    {
        // GetInput();

        if (gameManager.player.GetComponent<CharacterMovement>().enemyTarget)
            transform.LookAt(gameManager.player.GetComponent<CharacterMovement>().enemyTarget.transform);
        else
        {
            //transform.LookAt(camera.transform);
            // Turn();
        }
    }


    void FixedUpdate()
    {
        //Run();
        //Jump();

        //rBody.velocity = transform.TransformDirection(velocity);
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
            //move
            //rBody.velocity = transform.forward * forwardInput * moveSetting.forwardVel;
            velocity.z = moveSetting.forwardVel * forwardInput;
        }
        else
        {
            //rBody.velocity = Vector3.zero;
            velocity.z = 0;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

}
