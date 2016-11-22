using UnityEngine;
using System.Collections;

public class MeleeEnemyMovement : MonoBehaviour {

    //Public Classes
    public AttackActive attackActive;

    //Public Member Variables
    public Animator anim;
    public Transform target, origin;
    public bool seePlayer, followingPlayer;
    public float damageDealt, turnSpeed, maxTurn, lineOfSight, maxSight, moveSpeed, hammerPowerBlock;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        // anim.SetBool("isIdle", true);
        transform.eulerAngles = new Vector3(0, 0, 0);

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        if(!seePlayer)
        {

            if (this.transform.position == origin.position)
            {
                anim.SetBool("isChasing", false);
                anim.SetBool("isIdle", true);
                if (!followingPlayer)
                {
                    transform.Rotate(0, turnSpeed, 0);
                    if ((transform.rotation.eulerAngles.y > maxTurn && transform.rotation.eulerAngles.y < 180) ||
                        (transform.rotation.eulerAngles.y < 360 - maxTurn && transform.rotation.eulerAngles.y > 180))
                    {
                        turnSpeed *= -1;
                    }
                }
                else
                {
                    transform.Rotate(0, turnSpeed, 0);
                    if(transform.rotation.eulerAngles.y < maxTurn )
                    {
                        followingPlayer = false;
                    }
                }
            }

            else
            {
                Vector3 direction = origin.position - this.transform.position;
                direction.y = 0;
                //  this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                transform.LookAt(origin);
                transform.position = Vector3.MoveTowards(transform.position, origin.position, moveSpeed);

            }
        }
        else
        {
            transform.LookAt(target);
            followingPlayer = true;

            if (Vector3.Distance(target.position, this.transform.position) < 10)
            {
                Vector3 direction = target.position - this.transform.position;
                if (direction.magnitude > 1)
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isChasing", true);
                    anim.SetBool("isAttacking", false);
                    this.transform.Translate(0, 0, moveSpeed);
                }
                else
                {
                    anim.SetBool("isChasing", false);
                    anim.SetBool("isAttacking", true);
                    if (attackActive.isAttacking)
                    {
                        if (target.GetComponent<Animator>().GetBool("HammerBlock"))
                        {
                            target.GetComponent<PlayerAttributes>().health -= damageDealt / hammerPowerBlock;
                        }
                        else
                            target.GetComponent<PlayerAttributes>().health -= damageDealt;
                    }
                }
            }
        }
        
        Vector3 sight = target.transform.position - transform.position;
        float dot = Vector3.Dot(sight, transform.right);
        
        if(dot < lineOfSight && dot > -lineOfSight)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hit, maxSight))
            {
                if (hit.collider.name == "Player")
                    seePlayer = true;
                else
                    seePlayer = false;

            }
            else
                seePlayer = false;
        }
        
    }
}
