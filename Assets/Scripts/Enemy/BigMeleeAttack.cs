using UnityEngine;
using System.Collections;

public class BigMeleeAttack : MonoBehaviour {

    public AttackActive attackActive;

	// Use this for initialization
	void Start () {
	
	}


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player" && attackActive.isAttacking)
        {
            col.transform.GetComponent<PlayerAttributes>().health -= 5;
        }
    }


}
