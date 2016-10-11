using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour
{

    //public member variables
    public GameObject pos1, pos2, pos3, bulletPrefab, spawnPoint;
    public Transform targetPos;
    public Transform target;
    public bool inRangeOfPlayer;
    public float speed, timePassed, timeWait;

    // Use this for initialization
    void Start()
    {
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
            timePassed = Time.time + 1;
            inRangeOfPlayer = true;
        }
        else if (Vector3.Distance(target.position, this.transform.position) < 10)
        {
            inRangeOfPlayer = true;
        }
        else
        {
            inRangeOfPlayer = false;
        }
        
        if (inRangeOfPlayer)
        {
            // Determines which way to turn towards enemy
            Vector3 targetDir = target.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);

            // If Enemy is facing player, it shoots player
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Vector3.Dot(forward, targetDir) > 0)
            {

                if (timePassed < Time.time)
                {
                    timePassed = Time.time + timeWait;
                    Vector3 bulletRotate = new Vector3(90, 0, 0);
                    GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
                    //bullet.transform.rotation = Quaternion.LookRotation(newDir);
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

            Vector3 targetDir = targetPos.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0.05f);
        }


    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRangeOfPlayer = true;
            timePassed = Time.time + 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRangeOfPlayer = false;
        }
    }
    */
}

