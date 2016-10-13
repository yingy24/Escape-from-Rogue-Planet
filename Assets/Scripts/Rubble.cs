using UnityEngine;
using System.Collections;

public class Rubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int childNum = transform.childCount;
            for(int i = 0; i < childNum; ++i)
            {
                transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                //transform.GetChild(i).GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}
