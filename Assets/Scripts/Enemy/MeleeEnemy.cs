using UnityEngine;
using System.Collections;

public class MeleeEnemy : MonoBehaviour {

    //Public Member Variables
    public Transform target, origin;
    public bool seePlayer;
    public float turnSpeed, maxTurn, lineOfSight, maxSight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if(!seePlayer)
        {
            if (this.transform.position == origin.position)
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

                Vector3 direction = origin.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                transform.position = Vector3.MoveTowards(transform.position, origin.position, 0.05f);
            }
        }
        else
        {
            transform.LookAt(target);

            if (Vector3.Distance(target.position, this.transform.position) < 10)
            {
                Vector3 direction = target.position - this.transform.position;
                if (direction.magnitude > 3)
                {
                    this.transform.Translate(0, 0, 0.03f);
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
