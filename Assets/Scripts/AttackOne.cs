using UnityEngine;
using System.Collections;

public class AttackOne : MonoBehaviour {

    public AttackCombo attackCombo;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Enemy" && attackCombo.isAttacking)
        {
            col.transform.parent.GetComponent<EnemyHealth>().health -= 5;
        }
    }
    /*
    void OnTriggerStay(Collider col)
    {
        if (col.transform.tag == "Enemy" && attackCombo.isAttacking)
        {
            col.transform.parent.GetComponent<EnemyHealth>().health -= 5;
        }
    }
    */
}
