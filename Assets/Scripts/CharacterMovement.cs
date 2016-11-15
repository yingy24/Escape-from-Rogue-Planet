using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    //Classes
    public PlayerAttributes playerAttributes;

    public Animator anim;

    public float runSpeed, sprintSpeed, sprintStamina;
    public float jumpVelocity;
    public Vector3 targetPos; // Used for camera target pos


    public GameObject enemyTarget; // which enemy the player is locked on
    public Transform cameraTarget; // camera target

    public bool isAttacking, isUsingEnergy;

    //private variables
    private bool moving = false;
    private float vMovement;
    private float hMovement;
    private float capSpeed;

    float rotationSpeed = 30;
    Vector3 inputVec, correctMove, targetDirection;
    Rigidbody rb;

    void Start()
    {
        //Get animator, rigidbody, and player attribute script from this object
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerAttributes = GetComponent<PlayerAttributes>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //if(enemyTarget == null)

        if (anim.GetBool("Attacking"))
            return;

        //Get movement inputs
        vMovement = Input.GetAxisRaw("Vertical");
        hMovement = Input.GetAxisRaw("Horizontal");

        //Normalize imputs
        inputVec = new Vector3(hMovement, 0, vMovement);
        inputVec = (inputVec.magnitude > 1.0f) ? inputVec.normalized : inputVec;

        //Apply inputs to animator
        anim.SetFloat("Input Z", inputVec.z);
        anim.SetFloat("Input X", inputVec.x);
        anim.SetFloat("RVelocity", rb.velocity.y);

        GetCameraRelativeMovement();
        RotateTowardMovementDirection();

        CheckMoving();
        Move();
        Jump();
        HandleCameraTarget();
    }

    
    void CheckMoving()
    {

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

        //Check Sprinting
        if (Input.GetButton("Sprint") && playerAttributes.stamina > 0)
        {
            anim.SetBool("Sprinting", true);
            if(anim.GetBool("Moving"))
            {
                playerAttributes.stamina -=  sprintStamina;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;
            }
        }
        else
        {
            anim.SetBool("Sprinting", false);
        }

        //Check if OnGround or InAir
        if (rb.velocity.y == 0 || Mathf.Abs(rb.velocity.y) < 0.01)
        {
            anim.SetBool("OnGround", true);
        }
        else
        {
            anim.SetBool("OnGround", false);
        }

        if (rb.velocity.y != 0 && Mathf.Abs(rb.velocity.y) > 0.01)
        {
            anim.SetBool("InAir", true);
        }
        else
        {
            anim.SetBool("InAir", false);
        }


    }

    void Move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") | moving)
                    {
                        if (!anim.GetBool("Sprinting"))
                        {
                            capSpeed = runSpeed;
                        }
                        else
                        {
                            capSpeed = sprintSpeed;
                        }

                        inputVec.x *= capSpeed * Time.deltaTime;
                        inputVec.z *= capSpeed * Time.deltaTime;

                        MoveCorrection();
                    }
                }
            }

        }
    }

    void MoveCorrection()
    {
        //Correcting vMovement & hMovement to move according to the camera
        if (hMovement == 0 || Mathf.Abs(vMovement) > Mathf.Abs(hMovement))
        {
            if (vMovement > 0)
            {
                correctMove.z = inputVec.z;
            }
            else
            {
                correctMove.z = -inputVec.z;
            }
        }
        if (vMovement == 0 || Mathf.Abs(vMovement) < Mathf.Abs(hMovement))
        {
            if (hMovement > 0)
            {
                correctMove.z = inputVec.x;
            }
            else
            {
                correctMove.z = -inputVec.x;
            }
        }

        transform.Translate(correctMove);

        //Zeroing out minute velocity
        if (Mathf.Abs(rb.velocity.x) != 0 | Mathf.Abs(rb.velocity.z) != 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (Mathf.Abs(rb.velocity.y) > 0 && Mathf.Abs(rb.velocity.y) < 0.01)
        {
          //  print("rb.Vel Y =" + rb.velocity.y);
        }

        //correct gravity and downward velocity
        //rb.AddForce(transform.up * -75, ForceMode.Impulse);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //rb.AddRelativeForce(new Vector3(0, jumpVelocity, 0));
            if (rb.velocity.y == 0 || Mathf.Abs(rb.velocity.y) < 0.3)
            {
                anim.SetTrigger("Jump");
                StartCoroutine(ApplyVelocity());
            }
        }
    }

    IEnumerator ApplyVelocity()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector3(0, jumpVelocity, 0);
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
        if (inputVec != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }

    void HandleCameraTarget()
    {
        if (enemyTarget == null)
        {
            targetPos = transform.position;
            cameraTarget.position = targetPos;

        }
        else
        {

            Vector3 direction = enemyTarget.transform.position - transform.position;
            direction.y = 0;

            float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);

            targetPos = direction.normalized * distance / 5;

            targetPos += transform.position;

            cameraTarget.position = targetPos;

            if (distance > 20)
            {
                enemyTarget = null;
            }

        }
    }

    public void StartedAttacking()
    {
        if (isUsingEnergy)
        {
            NewStartedAttacking();
        }
        else
        {
            playerAttributes.stamina -= 5;
            anim.SetBool("Attacking", true);
            isAttacking = true;
        }
    }

    public void EndedAttacking()
    {
        isAttacking = false;
        isUsingEnergy = false;
        anim.SetBool("Attacking", false);
    }

    public void NewStartedAttacking()
    {
        playerAttributes.currentWeaponEnergy -= 2;
        anim.SetBool("Attacking", true);
        isAttacking = true;
    }

}
