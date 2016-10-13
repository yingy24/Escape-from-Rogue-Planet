using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    // Class Scripts
    public AttackCombo attackCombo;

    //Public Member Variables
    public Slider playerHealth, playerStamina; // Used to display the UI for now...
    public GameObject sword; // Sword object
    public float health, stamina, regainStaminaTime, restTimer, staminaRegain;
    public bool isDead, hasWon, hasWeapon;

    // Private Member Variables
   // private float restTimer; //Reset the time for the player to regain stamina


    // Use this for initialization
    void Awake () {
        attackCombo = GetComponent<AttackCombo>();
        hasWeapon = false;
        isDead = false;
        hasWon = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            isDead = true;
        }
        if (health > 100)
        {
            health = 100;
        }

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
            if (restTimer > regainStaminaTime)
            {
                regainStaminaTime = 0.1f;
                restTimer = 0;
                stamina += staminaRegain;
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

        if(other.tag == "HealthPickUp")
        {
            if(health <= 100)
            {
                health += 5;
            }
            Destroy(other.gameObject);
        }

        if(other.tag == "WinningPlatform")
        {
            hasWon = true;
        }
    }
}
