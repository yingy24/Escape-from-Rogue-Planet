using UnityEngine;
using System.Collections;

public class AttackCombo : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;

    // Public Member Variables
    public Animator anim;
    public string[] comboParams;
    public bool hasWeapon, isAttacking;
    public float attackRate;
    public float attackDamage = 10f;

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
                "Attack1",
                "Attack2",
                "Attack3"
            };

        }
        isAttacking = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length && hasWeapon && playerAttributes.stamina > 0)
        {
            anim.SetTrigger(comboParams[comboIndex]);
            comboIndex++;
            playerAttributes.stamina -= 5;
            playerAttributes.restTimer = 0;

            restTimer = 0f;
            
        }
        if(comboIndex > 0)
        {
            restTimer += Time.deltaTime;
            if(restTimer > attackRate)
            {
                anim.SetTrigger("AttackReset");
                comboIndex = 0;
            }
        }
    }

    void LateUpdate()
    {
        //anim.SetBool("Attacking", false);

    }

    void StartedAttacking()
    {
        isAttacking = true;
    }
    void EndedAttacking()
    {
        isAttacking = false;
    }

    public void SwordObtained()
    {
        hasWeapon = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
        }

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
        }

    }
    
}
