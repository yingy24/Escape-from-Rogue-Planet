using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAttributes : MonoBehaviour {

    // Class Scripts
    public AttackCombo attackCombo;

    //Public Member Variables
    public Animator anim;
    public Slider playerHealth, playerStamina, weaponEnergy; // UI Displays
    public List <GameObject> weapons; // Sword object
    public List<bool> weaponsObtained, weaponsActive;
    public float health, stamina, currentWeaponEnergy, regainStaminaTime, restTimer, staminaRegain;
    public bool isDead, hasWon, hasWeapon;

    // Private Member Variables
    // private float restTimer; //Reset the time for the player to regain stamina


    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        attackCombo = GetComponent<AttackCombo>();
        hasWeapon = true; //change this to false after testing!!
        isDead = false;
        hasWon = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            isDead = true;
            return;
        }
        if (health > 100)
        {
            health = 100;
        }

        // Weapon Select
        if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetAxis("DpadX") == -1)
        {
            if (!weaponsObtained[0])
                return;

           else if(weaponsObtained[0])
            {
                anim.SetInteger("WeaponState", 0);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[0].SetActive(true);
                weaponsActive[0] = true;
                weaponsActive[1] = false;
                weaponsActive[2] = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxis("DpadY") == 2)
        {
            if (!weaponsObtained[1])
                return;

            else if (weaponsObtained[1])
            {
                anim.SetInteger("WeaponState", 1);
                weapons[0].SetActive(false);
                weapons[2].SetActive(false);
                weapons[1].SetActive(true);
                weaponsActive[0] = false;
                weaponsActive[1] = true;
                weaponsActive[2] = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("DpadX") == 1)
        {
            if (!weaponsObtained[2])
                return;

           else if (weaponsObtained[2])
            {
                anim.SetInteger("WeaponState", 2);
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(true);
                weaponsActive[0] = false;
                weaponsActive[1] = false;
                weaponsActive[2] = true;
            }
        }


        // Stamina check and stamina to regain
        if (stamina < 20)
        {
            restTimer += Time.deltaTime;    // resets the counter for time
            // if time is greater than the set time, gains 1 stamina per second
            if (restTimer > regainStaminaTime)
            {
                regainStaminaTime = 0.1f;
                restTimer = 0;
                stamina += staminaRegain;
            }
        }

        playerHealth.value = health;                 //Sets the UI Health to the player Health
        playerStamina.value = stamina;               //Sets the UI Stamina to the player Stamina
        weaponEnergy.value = currentWeaponEnergy;   // Sets the UI Energy Bar

    }

    void OnTriggerEnter(Collider other)
    {
        // Interactions with sword to be "Picked up"
        if(other.tag == "Sword")
        {
            hasWeapon = true;
            weapons[0].SetActive(true);
            weaponsObtained[0] = true;
            attackCombo.SwordObtained();
            weaponsActive[0] = true;
            weaponsActive[1] = false;
            weaponsActive[2] = false;
            Destroy(other.gameObject);

        }

        if (other.tag == "Axe")
        {
            hasWeapon = true;
            weapons[1].SetActive(true);
            weaponsObtained[1] = true;
            attackCombo.SwordObtained();
            weaponsActive[0] = false;
            weaponsActive[1] = true;
            weaponsActive[2] = false;
            Destroy(other.gameObject);
        }

        if (other.tag == "Gun")
        {
            hasWeapon = true;
            weapons[2].SetActive(true);
            weaponsObtained[2] = true;
            attackCombo.SwordObtained();
            weaponsActive[0] = false;
            weaponsActive[1] = false;
            weaponsActive[2] = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "HealthPickUp")
        {
            if(health <= 100)
            {
                health += 25;
            }
            Destroy(other.gameObject);
        }

        if(other.tag == "WinningPlatform")
        {
            hasWon = true;
        }
    }
}
