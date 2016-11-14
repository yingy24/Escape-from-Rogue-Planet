using UnityEngine;
using System.Collections;

public class WeaponAnimation : MonoBehaviour {

    private Animator anim;
    
    // Use this for initialization
	void Awake () {
        //player = GameObject.FindWithTag("Player");
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if ()
	}
}
