using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{

    //Classes
    public PlayerAttributes playerAttributes;

    [System.Serializable]
    public class MoveSettings
    {
        public float jumpVel = 12;
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
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    public Animator anim;
    //public Transform camTransform;
    public float runSpeed;
    public float sprintSpeed;

    private bool moving = false;
    private float vMovement;
    private float hMovement;
    private float capSpeed;

    float rotationSpeed = 30;
    Vector3 targetDirection;
    Vector3 velocity = Vector3.zero;
    Rigidbody rb;

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        print("Waited 0.6s");
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerAttributes = GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get movement inputs
        vMovement = Input.GetAxisRaw("Vertical");
        hMovement = Input.GetAxisRaw("Horizontal");

        //Apply inputs to animator
        anim.SetFloat("Input Z", vMovement);
        anim.SetFloat("Input X", -(hMovement));

        GetCameraRelativeMovement();
        RotateTowardMovementDirection();

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") | moving)
            {

                if (!anim.GetBool("Sprinting"))
                {
                    capSpeed = runSpeed;
                    //Running
                    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 | Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5 && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5)
                    {
                        capSpeed = Mathf.Sqrt(capSpeed);
                    }
                    else
                    {
                        capSpeed = runSpeed;
                    }
                    vMovement *= capSpeed * Time.deltaTime;
                    hMovement *= capSpeed * Time.deltaTime;
                }
                else
                {
                    capSpeed = sprintSpeed;
                    //Sprinting
                    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 | Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5 && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5)
                    {
                        capSpeed = Mathf.Sqrt(capSpeed);
                    }
                    else
                    {
                        capSpeed = sprintSpeed;
                    }
                    vMovement *= capSpeed * Time.deltaTime;
                    hMovement *= capSpeed * Time.deltaTime;
                }
                MoveCorrection();
            }
        }

        //OLD - Character rotation when move
        //transform.localEulerAngles = new Vector3(0, camTransform.eulerAngles.y, 0);
    }

    void FixedUpdate()
    {
        CheckMoving();
        Jump();

        rb.velocity = transform.TransformDirection(velocity);
    }

    void CheckMoving()
    {
        //Check if on ground
        if (Grounded())
        {
            //print("grounded");
            anim.SetBool("OnGround", true);
            anim.SetBool("InAir", false);
        }
        else
        {
            //print("Not grounded");
            anim.SetBool("OnGround", false);
            anim.SetBool("InAir", true);
        }

        //Check movement on XZ plane
        if (vMovement != 0 | hMovement != 0)
        {
            moving = true;
            anim.SetBool("Moving", true);
            anim.SetBool("Running", true);
        }
        else
        {
            moving = false;
            anim.SetBool("Moving", false);
            anim.SetBool("Running", false);
        }

        if (Input.GetButton("Sprint") && playerAttributes.stamina > 0)
        {
            anim.SetBool("Sprinting", true);
            if(anim.GetBool("Moving"))
            {
                playerAttributes.stamina -= 0.5f;
            }
        }
        else
        {
            anim.SetBool("Sprinting", false);
        }
    }

    void MoveCorrection()
    {

        //Correcting vMovement & hMovement
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.Translate(hMovement, 0, vMovement);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.Translate(-hMovement, 0, -vMovement);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(-vMovement, 0, hMovement);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(vMovement, 0, -hMovement);
        }
    }

    void Jump()
    {
    
        if (Input.GetAxisRaw(inputSetting.JUMP_AXIS) > 0 && Grounded() && playerAttributes.stamina >= 0)
       // if (Input.GetButtonDown("Jump") && Grounded())
        {
            print("Jumping Should be activated"); 
            //Jump
            anim.SetTrigger("Jump");

            //StartCoroutine("WaitForAnimation");
            velocity.y = moveSetting.jumpVel;
            playerAttributes.stamina -= 3f;
            playerAttributes.regainStaminaTime = 1f;
        }
        else if (Input.GetAxisRaw(inputSetting.JUMP_AXIS) == 0 && Grounded())
        {
            //zero out velocity.y
            velocity.y = 0;
        }
        else
        {
            //Decrease velocity.y
            velocity.y -= physSetting.downAccel;
        }
    }

    //-------------------------------------------------------------------
    //The following scripts are borrowed from WarriorAnimationDemoFREE.cs
    //-------------------------------------------------------------------

    //converts control input vectors into camera facing vectors
    void GetCameraRelativeMovement()
    {
        Transform cameraTransform = Camera.main.transform;

        // Forward vector relative to the camera along the x-z plane   
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        //directional inputs
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // Target direction relative to the camera
        targetDirection = h * right + v * forward;
    }

    //face character along input direction
    void RotateTowardMovementDirection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 | Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }

}
