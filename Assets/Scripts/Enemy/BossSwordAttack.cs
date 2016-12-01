using UnityEngine;
using System.Collections;

public class BossSwordAttack : MonoBehaviour {

    //public GameObject target;
    public float damageDealt, attackRate;
    public bool canSeePlayer, canAttack, isAttacking;

    private Animator anim;
    private BossAttribute bossAttribute;

    [SerializeField]
    private string[] comboParam;
    private float timePassed;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        bossAttribute = GetComponent<BossAttribute>();
        timePassed = Time.time;

        if (comboParam == null || (comboParam != null && comboParam.Length == 0))
        {
            comboParam = new string[]
            {
                "SwordCombo1",
                "SwordCombo2",
                "SwordCombo3"
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossAttribute.usingSword)
            return;

        Vector3 direction = bossAttribute.target.transform.position - this.transform.position;

        if (direction.magnitude < 10 && !canSeePlayer)
        {
            canSeePlayer = true;
            // print("close by");
        }
        else if (direction.magnitude <= 2 && canSeePlayer)
        {
            anim.SetBool("isChasing", false);
            canAttack = true;
        }
        else if (direction.magnitude > 10)
        {
            canSeePlayer = false;
            canAttack = false;
        }

        /*
        if (Vector3.Distance(bossAttribute.target.transform.position, transform.position) < 10 && !canSeePlayer)
        {
            canSeePlayer = true;
           // print("close by");
        }
        else if (Vector3.Distance(bossAttribute.target.transform.position, transform.position) <= 1 && canSeePlayer)
        {
            canAttack = true;
        }
        else if(Vector3.Distance(bossAttribute.target.transform.position, transform.position) > 10)
        {
            canSeePlayer = false;
            canAttack = false;
        }
        */
        int randomNum = Random.Range(0, 3);

        if (canAttack)
        {
            if (timePassed < Time.time)
            {
                anim.SetTrigger(comboParam[randomNum]);
                timePassed = Time.time + attackRate;
            }
        }
    }
    public void StartedAttacking()
    {
        isAttacking = true;
    }

    public void DoneAttacking()
    {
        isAttacking = false;
    }
}






