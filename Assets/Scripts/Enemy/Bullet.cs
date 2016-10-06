using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    // Public Member Variables
    public GameObject player;
    public Transform playerT;
    public float speed, damage,timeAlive, timeToDie;


	// Use this for initialization
	void Start () {
        timeAlive = Time.time;
        timeToDie = Time.time + 2;
        playerT = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        // print(this.name + " " + playerT.position);
        // transform.position = Vector3.MoveTowards(transform.position, playerT.position, speed*Time.deltaTime);
        Rigidbody temp_rigid = this.GetComponent<Rigidbody>();
        temp_rigid.AddForce(transform.forward * speed);

        timeAlive = Time.time;
        /*
        if (timeAlive > timeToDie)
        {
            Destroy(this.gameObject);
        }
        */

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            other.GetComponent<PlayerAttributes>().health -= 5;
        }
    }
}

