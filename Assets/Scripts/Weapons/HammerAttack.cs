using UnityEngine;
using System.Collections;

public class HammerAttack : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    public CharacterMovement cMovement;
    public AttackOne attackScript;
    

    // Public Member Variables
    public Animator anim;
    public Animation currentAnima;
    public GameObject hammerHead;
    public GameObject shield;
    public string[] comboParams;
    public bool isAttacking;
    public float attackRate, weaponEnergyReduction;
    public float attackDamage;

    //Private Member Variables
    private int comboIndex = 0; //Counter for which Animation is playing
    private float restTimer; //Reset the time if the player didn't press the attack quick enough


    void Awake()
    {
        playerAttributes = GetComponent<PlayerAttributes>();    // Gets the info of player attributes
        anim = GetComponent<Animator>();                        // gets the animator info

        // Collecting the animations for attacking if there isn't anything already set
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
        {
            comboParams = new string[]
            {
                "HAttack1",
                "HAttack2"
            };

        }
        isAttacking = false;
        hammerHead.SetActive(true);
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        if (!playerAttributes.weaponsActive[1])
        {
            return;
        }


        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length &&/*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.stamina > 5)
        {
            attackScript.damageDealt = attackDamage;
            anim.SetTrigger(comboParams[comboIndex]);
            comboIndex++;

            playerAttributes.restTimer = 0;
            playerAttributes.regainStaminaTime = 1;
            restTimer = 0f;

        }
        
       else if (Input.GetButtonDown("Fire2") && comboIndex < comboParams.Length &&/*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > 2)
        {
            attackScript.damageDealt = 7;
            anim.SetTrigger(comboParams[comboIndex]);
            comboIndex++;
            cMovement.isUsingEnergy = true;
            restTimer = 0f;

        }

       else if (Input.GetButton("Fire2") && playerAttributes.currentWeaponEnergy > 0)
        {
            anim.SetBool("Attacking", true);
            anim.SetBool("HammerBlock", true);
            hammerHead.SetActive(false);
            shield.SetActive(true);
            playerAttributes.currentWeaponEnergy -= Time.deltaTime * weaponEnergyReduction;
        }
        else
        {
            anim.SetBool("HammerBlock", false);
            hammerHead.SetActive(true);
            shield.SetActive(false);
        }

        if (comboIndex > 0)
        {
            restTimer += Time.deltaTime;
            if (restTimer > attackRate)
            {
                anim.SetTrigger("AttackReset");
                comboIndex = 0;
            }
        }

        if(Input.GetButton("Fire2") && playerAttributes.currentWeaponEnergy > 0 )
        {
            anim.SetBool("HammerBlock", true);
            hammerHead.SetActive(false);
            shield.SetActive(true);
            playerAttributes.currentWeaponEnergy -= Time.deltaTime * weaponEnergyReduction;
        }
        else
        {
            anim.SetBool("HammerBlock", false);
            hammerHead.SetActive(true);
            shield.SetActive(false);
        }
    }

    public void DoneBlocking()
    {
        anim.SetBool("Attacking", false);
    }
}
