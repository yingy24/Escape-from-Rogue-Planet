using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    // Class Scripts
    public AttackCombo attackCombo;

    //Public Member Variables
    public Slider playerHealth, playerStamina, weaponEnergy; // UI Displays
    public GameObject sword; // Sword object
    public float health, stamina, currentWeaponEnergy, regainStaminaTime, restTimer, staminaRegain;
    public bool isDead, hasWon, hasWeapon;

    // Private Member Variables
    // private float restTimer; //Reset the time for the player to regain stamina


    // Use this for initialization
    void Awake () {
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
            print("1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxis("DpadY") == 2)
        {
            print("2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("DpadX") == 1)
        {
            print("3");
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
            sword.SetActive(true);
            attackCombo.SwordObtained();
            Destroy(other.gameObject);
        }

        if(other.tag == "HealthPickUp")
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
