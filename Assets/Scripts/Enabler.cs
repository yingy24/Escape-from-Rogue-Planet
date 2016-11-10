using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enabler : MonoBehaviour {


    public List<GameObject> enableObjects;
    public bool enableAll;


   // public AttackCombo attackCombo;
   // public GameObject isOff, isOn, ElvButton;
    
    // Use this for initialization
	void Start () {
        foreach (GameObject gO in enableObjects)
        {
            gO.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
        if(enableAll)
        {
            foreach(GameObject gO in enableObjects)
            {
                gO.SetActive(true);
            }
        }

	}

    void OnTriggerStay(Collider other)
    {



        /*
        if(attackCombo.isAttacking)
        {
            isOff.SetActive(false);
            isOn.SetActive(true);
            ElvButton.SetActive(true);
        }
        */
    }

}
