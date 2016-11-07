using UnityEngine;
using System.Collections;

public class CameraRaycast : MonoBehaviour {

    float distance = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            distance = hit.distance;
        }
	}
}
