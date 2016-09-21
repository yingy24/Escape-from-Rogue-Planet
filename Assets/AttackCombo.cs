using UnityEngine;
using System.Collections;

public class AttackCombo : MonoBehaviour {

    public Animator animator;
    //public float comboTime;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire2"))
        {
            animator.SetTrigger("Attack2Trigger");
        }

        if (Input.GetButton("Fire3"))
        {
            animator.SetTrigger("Attack3Trigger");
        }

        //comboTime = Time.deltaTime - comboTime;
        /*
        if (animator.GetBool("Attacking") == true)
        {
            if (Input.GetButton("Fire1"))
            {
                animator.SetTrigger("Attack2Trigger");
            }
        }
        */

        /*
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") | animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            animator.SetBool("IsAttacking", false);
        }
        */
    }
}
