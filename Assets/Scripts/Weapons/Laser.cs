using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    private LineRenderer lineR;
    public float damage, maxDistance;

	// Use this for initialization
	void Start () {

        lineR = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.tag == "Enemy")
            {
                lineR.SetPosition(1, new Vector3(0, 0, hit.distance));
                hit.collider.GetComponent<EnemyHealth>().health -= damage;
            }
            else
            {
                lineR.SetPosition(1, new Vector3(0, 0, maxDistance));
            }
        }

	}
}
