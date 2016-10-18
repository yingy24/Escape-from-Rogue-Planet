using UnityEngine;
using System.Collections;

public class AttackActive : MonoBehaviour {

    public bool isAttacking;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartedAttacking()
    {
        isAttacking = true;
    }

    public void DoneAttacking()
    {
        isAttacking = false;
    }

}
