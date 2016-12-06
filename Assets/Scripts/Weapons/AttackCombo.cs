using UnityEngine;
using System.Collections;

public class AttackCombo : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    private CharacterMovement cMovement;

    // Public Member Variables
    public Animator anim;
    public Animation currentAnima;
    public string[] comboParams;
    public bool isAttacking;
    public float attackRate;
    public float attackDamage, staminaReduction;

    //Private Member Variables
    private int comboIndex = 0; //Counter for which Animation is playing
    private float restTimer; //Reset the time if the player didn't press the attack quick enough


    void Awake()
    {
        playerAttributes = GetComponent<PlayerAttributes>();    // Gets the info of player attributes
        anim = GetComponent<Animator>();                        // gets the animator info
        cMovement = GetComponent<CharacterMovement>();          // gets the character movement
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
    void FixedUpdate()
    {

        if (!playerAttributes.weaponsActive[0])
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.stamina > 5)
        {
            anim.SetTrigger(comboParams[comboIndex]);
            comboIndex++;
            playerAttributes.restTimer = 0;
            playerAttributes.regainStaminaTime = 1;
            restTimer = 0f;

        }

        else if (comboIndex > 0)
        {
            restTimer += Time.deltaTime;
            if (restTimer > attackRate)
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


    public void SwordAttacking()
    {
        playerAttributes.stamina -= staminaReduction;
        anim.SetBool("Attacking", true);
        isAttacking = true;
        cMovement.isAttacking = true;
    }
    
    public void SwordObtained()
    {
        // hasWeapon = true;
    }
}
