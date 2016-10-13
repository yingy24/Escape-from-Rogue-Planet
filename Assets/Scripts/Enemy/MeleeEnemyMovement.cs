using UnityEngine;
using System.Collections;

public class MeleeEnemyMovement : MonoBehaviour {

    //Public Member Variables
    public Animator anim;
    public Transform target, origin;
    public bool seePlayer, followingPlayer;
    public float turnSpeed, maxTurn, lineOfSight, maxSight, moveSpeed;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if(!seePlayer)
        {

            if (this.transform.position == origin.position)
            {
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
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
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
                    this.transform.Translate(0, 0, moveSpeed);
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

        /*
        if (Vector3.Distance(target.position, this.transform.position) < 10)
        {
            Vector3 direction = target.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if(direction.magnitude > 3)
            {
                this.transform.Translate(0, 0, 0.03f);
            }
        }
        */

    }
}
