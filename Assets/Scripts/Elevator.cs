using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    //Public Classes
    public Enabler enabler;

    //Public Member Variables
    public GameObject moveHere;
    public bool move;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(move)
            transform.position = Vector3.MoveTowards(transform.position, moveHere.transform.position, 0.05f);
    }

    void OnTriggerStay(Collider other)
    {
        if (enabler.isOn)
        {
            if (Input.GetButtonDown("Fire3")) // Assign the button to use with the elvator.  
            {
                move = true;
            }

        }
    }
}
