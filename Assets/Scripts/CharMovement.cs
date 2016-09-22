using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

    public Animator animator;
    public float runSpeed;
    public float addSprintSpeed;
    public float jumpAmount;

    // Update is called once per frame
    void Update()
    {

        //Sprinting
        if (Input.GetButton("Sprint"))
        {
            animator.SetBool("Sprinting", true);
        }
        else
        {
            animator.SetBool("Sprinting", false);
        }

        //if (!animator.GetBool("Attack1Trigger"))
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack 1"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") | animator.GetBool("Running") | animator.GetBool("Sprinting"))
            {
                //Running
                Vector3 pos = transform.position;
                pos.z += Input.GetAxis("Vertical") * runSpeed * Time.deltaTime;
                pos.x += Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("sprint"))
                {
                    pos.z += Input.GetAxis("Vertical") * addSprintSpeed * Time.deltaTime;
                    pos.x += Input.GetAxis("Horizontal") * addSprintSpeed * Time.deltaTime;
                }

                //Jumping
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Jump button");
                    animator.SetTrigger("JumpTrigger");
                    //animator.SetBool("InAir", true);
                    //pos.y = transform.Translate(Vector3.up * jumpAmount* Time.deltaTime, Space.World);
                }

                transform.position = pos;
            }
        }
    }
}
