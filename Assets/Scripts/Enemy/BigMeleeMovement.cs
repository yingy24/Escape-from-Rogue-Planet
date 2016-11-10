using UnityEngine;
using System.Collections;

public class BigMeleeMovement : MonoBehaviour
{

    //public member variables
    public Animator anim;
    public GameObject pos1, pos2, pos3;
    public Transform targetPos;
    public Transform target;
    public bool inRangeOfPlayer;
    public float speed, timePassed, timeWait, movementSpeed;

    // Use this for initialization
    void Start()
    {
      //  anim = GetComponent<Animator>();
        timePassed = Time.time;
        targetPos = pos2.transform;
        Vector3 targetDir = targetPos.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);

        //target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, this.transform.position) < 10 && !inRangeOfPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0);
           // anim.SetBool("isAttackIdle", true);
            timePassed = Time.time + 1;
            inRangeOfPlayer = true;
        }
        else if (Vector3.Distance(target.position, this.transform.position) < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0);
          //  anim.SetBool("isAttackIdle", true);
            inRangeOfPlayer = true;
        }
        else
        {
            inRangeOfPlayer = false;
            anim.SetBool("isPatrol", true);
            anim.SetBool("isChasing", false);
            anim.SetBool("isAttackIdle", false);
        }
        
        if (inRangeOfPlayer)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isChasing", true);
            // Determines which way to turn towards enemy
            Vector3 targetDir = target.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);

            // If Enemy is facing player, it shoots player
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            // transform.position = Vector3.MoveTowards(this.transform.position, target.position, movementSpeed * Time.deltaTime);
            if (Vector3.Dot(forward, targetDir) > 1)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, target.position, movementSpeed * Time.deltaTime);

                //if (timePassed < Time.time)
                //  {
                //     timePassed = Time.time + timeWait;

                //  }
            }
            else if (Vector3.Dot(forward, targetDir) <= 1.5)
            {
                anim.SetBool("isChasing", false);
                anim.SetBool("isAttackIdle", true);

                if (timePassed < Time.time)
                {
                    anim.SetTrigger("isAttacking");
                    timePassed = Time.time + timeWait;
                }
            }

        }

        if (!inRangeOfPlayer)
        {
            if (transform.position == pos1.transform.position)
            {
                targetPos = pos2.transform;

            }
            else if (transform.position == pos2.transform.position)
            {
                targetPos = pos1.transform;

            }
            anim.SetBool("isPatrol", true);
            Vector3 targetDir = targetPos.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, movementSpeed * Time.deltaTime);
        }


    }
}

