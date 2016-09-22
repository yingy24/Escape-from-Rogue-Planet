﻿using UnityEngine;
using System.Collections;

public class AttackCombo : MonoBehaviour {

    public Animator animator;
    public float attackDamage = 10f;
    public bool withinEnemRange;
    public Transform enemy;
    public bool hasWeapon;
    //public float comboTime;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attacking", true);
            animator.SetTrigger("Attack1Trigger");

        }


        //comboTime = Time.deltaTime - comboTime;
        if (animator.GetBool("Attacking") == true)
        {
            /*
            if (Input.GetButton("Fire1"))
            {
                animator.SetTrigger("Attack2Trigger");
            }
            */
        }

        /*        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") | animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            animator.SetBool("IsAttacking", false);
        }
        */

    }

    void LateUpdate()
    {
        animator.SetBool("Attacking", false);
    }

    void AttackOne()
    {
        if (withinEnemRange)
        {
            float e = enemy.GetComponent<EnemyHealth>().health;
            e -= attackDamage;
            enemy.GetComponent<EnemyHealth>().health = e;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemy = collision.transform;
            withinEnemRange = true;
        }

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemy = collision.transform;
            withinEnemRange = true;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            withinEnemRange = false;
        }

    }
}
