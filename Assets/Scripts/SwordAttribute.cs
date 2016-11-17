using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwordAttribute : MonoBehaviour {

    // Public Member Variables
    public PlayerAttributes playerAttribute;
    public float enegyDrainage, healthRegain;
    


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(!this.gameObject)
        {
            return;
        }

        if (playerAttribute.currentWeaponEnergy > 0 && playerAttribute.health < 100)
        {
            if(Input.GetMouseButton(1) || Input.GetButton("SpecialAbility"))
            {
                playerAttribute.currentWeaponEnergy -= Time.deltaTime * enegyDrainage;
                playerAttribute.health += Time.deltaTime * healthRegain;
            }
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
