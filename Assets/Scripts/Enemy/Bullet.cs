using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    // Public Member Variables
    public GameObject player;
    public float speed, damage,timeAlive, timeToDie;


	// Use this for initialization
	void Start () {
        timeAlive = Time.time;
        timeToDie = Time.time + 2;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        timeAlive = Time.time;
        if (timeAlive > timeToDie)
        {
            Destroy(this.gameObject);
        }

        this.GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position).normalized * speed;
        Vector3 vel = this.GetComponent<Rigidbody>().velocity;
        vel.y = 0;
        this.GetComponent<Rigidbody>().velocity = vel;
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

