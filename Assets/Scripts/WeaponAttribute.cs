using UnityEngine;
using System.Collections;

public class WeaponAttribute : MonoBehaviour {

    // Public Member Variables
    public AttackCombo attackCombo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
                print("hitting the enemy");

        }

    }
    
    public void TestFunction()
    {

    }



}
