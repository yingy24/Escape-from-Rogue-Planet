using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    //Public Member Variables
    public GameObject sword;
    public float health;
    public bool hasWeapon;
    public Slider playerHealth;

    // Use this for initialization
    void Start () {
        hasWeapon = false;
	}
	
	// Update is called once per frame
	void Update () {
       
        if (!hasWeapon)
        {
            sword.SetActive(false);
        }
        else if(hasWeapon)
        {
            sword.SetActive(true);
        }
        playerHealth.value = health;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {

        }

        if(other.tag == "Sword")
        {
            hasWeapon = true;
            Destroy(other.gameObject);
        }

    }
}
