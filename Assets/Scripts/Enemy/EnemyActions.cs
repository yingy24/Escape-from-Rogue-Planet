using UnityEngine;
using System.Collections;

public class EnemyActions : MonoBehaviour
{

    //public member variables
    public GameObject pos1, pos2, pos3;
    public Transform targetPos;
    public Transform target;
    public bool inRangeOfPlayer;
    public float speed;

    // Use this for initialization
    void Start()
    {
        targetPos = pos2.transform;
        Vector3 targetDir = targetPos.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRangeOfPlayer)
        {
            Vector3 targetDir = target.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);
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
            //newDir.y = 0;
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0.05f);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRangeOfPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRangeOfPlayer = false;
        }
    }
}

