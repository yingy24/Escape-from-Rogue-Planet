using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponAttribute : MonoBehaviour {

    // Public Member Variables
    public AttackCombo attackCombo;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(!this.gameObject)
        {
            return;
        }



	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
        }

    }
    
    public void TestFunction()
    {

    }



}
