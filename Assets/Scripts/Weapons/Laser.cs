using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    private LineRenderer lineR;
    public float damage, maxDistance, speed;
    public float timeAlive, timeToDie;

    // Use this for initialization
    void Start () {

        lineR = GetComponent<LineRenderer>();

        timeAlive = Time.time;
        timeToDie = Time.time + 2;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * speed * Time.deltaTime;

        timeAlive = Time.time;

        if (timeAlive > timeToDie)
        {
            Destroy(this.gameObject);
        }

        /*
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.tag == "Enemy")
            {
                //lineR.SetPosition(1, new Vector3(0, 0, hit.distance));
                hit.collider.GetComponent<EnemyHealth>().health -= damage;
            }
            else if(hit.collider.tag == "Player")
            {
                print("sup player");
                //hit.collider.GetComponent<PlayerAttributes>().health -= damage;
                //lineR.SetPosition(1, new Vector3(0, 0, maxDistance));
            }
        }
         */

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().health -= damage;
            Destroy(this.gameObject);
            // GamePad.SetVibration(PlayerIndex.One, 0, 1);
            //  StartCoroutine(ZeroVibrateAndSelfDestruct());
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<PlayerAttributes>().health -= damage;
            // GamePad.SetVibration(PlayerIndex.One, 0, 0);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Boss")
        {
            print(other.name);
            other.GetComponent<BossAttribute>().health -= damage;
            // GamePad.SetVibration(PlayerIndex.One, 0, 0);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
