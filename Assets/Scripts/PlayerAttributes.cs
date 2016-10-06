﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    // Class Scripts
    public AttackCombo attackCombo;

    //Public Member Variables
    public Slider playerHealth, playerStamina; // Used to display the UI for now...
    public GameObject sword; // Sword object
    public float health, stamina, regainStamina, restTimer;
    public bool hasWeapon;

    // Private Member Variables
   // private float restTimer; //Reset the time for the player to regain stamina


    // Use this for initialization
    void Awake () {
        attackCombo = GetComponent<AttackCombo>();
        hasWeapon = true;
	}

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the player has sword
        // if player has sword, then it shows the sword, if not it doesn't show
        if (!hasWeapon)
        {
            sword.SetActive(false);
        }
        else if (hasWeapon)
        {
            sword.SetActive(true);
        }

        if (stamina < 20)
        {
            restTimer += Time.deltaTime;    // resets the counter for time
            // if time is greater than the set time, gains 1 stamina per second
            if (restTimer > regainStamina)
            {
                restTimer = 0;
                stamina += 1;
            }
        }

        playerHealth.value = health;        //Sets the UI Health to the player Health
        playerStamina.value = stamina;      //Sets the UI Stamina to the player Stamina

    }

    void OnTriggerEnter(Collider other)
    {
        // Interactions with sword to be "Picked up"
        if(other.tag == "Sword")
        {
            hasWeapon = true;
            attackCombo.SwordObtained();
            Destroy(other.gameObject);
        }
    }
}
