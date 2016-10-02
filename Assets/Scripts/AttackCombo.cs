using UnityEngine;
using System.Collections;

public class AttackCombo : MonoBehaviour {

    public Animator anim;
    /// <summary>
    public string[] comboParams;
    public float attackRate;
    private int comboIndex = 0;
    private float restTimer;
    /// </summary>
    public float attackDamage = 10f;
    public bool hasWeapon;
    //public float comboTime;

    void Awake()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
        {
            comboParams = new string[]
            {
                "Attack1",
                "Attack2",
                "Attack3"
            };

        }
        anim = GetComponent<Animator>();
        hasWeapon = true;
    }


    // Update is called once per frame
    void Update()
    {
        hasWeapon = GetComponent<PlayerAttributes>().hasWeapon;
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length && hasWeapon)
        {
            Debug.Log(comboParams[comboIndex] + " Trigged");
            anim.SetTrigger(comboParams[comboIndex]);
            comboIndex++;
            restTimer = 0f;


        }
        if(comboIndex > 0)
        {
            restTimer += Time.deltaTime;
            if(restTimer > attackRate)
            {
                anim.SetTrigger("Reset");
                comboIndex = 0;
            }
        }

    }

    void LateUpdate()
    {
        //anim.SetBool("Attacking", false);

    }
    
    void AttackOne()
    {

        /*
        if (withinEnemRange)
        {
            float e = enemy.GetComponent<EnemyHealth>().health;
            e -= attackDamage;
            enemy.GetComponent<EnemyHealth>().health = e;
        }
        */

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
