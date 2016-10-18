using UnityEngine;
using System.Collections;

public class Enabler : MonoBehaviour {

    public AttackCombo attackCombo;
    public GameObject isOff, isOn;
    
    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(attackCombo.isAttacking)
        {
            isOff.SetActive(false);
            isOn.SetActive(true);
        }
    }
}
