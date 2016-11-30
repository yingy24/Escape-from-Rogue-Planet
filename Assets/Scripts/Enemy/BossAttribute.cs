using UnityEngine;
using System.Collections;

public class BossAttribute : MonoBehaviour
{
    public GameObject target, sword, gun;
    public float health, energy, moveSpeed;
    public bool usingGun, usingSword;

    private Animator anim;
    private BossSwordAttack bossSwordAttack;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        bossSwordAttack = GetComponent<BossSwordAttack>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!anim)
        {
            print("ANIMATOR NOT FOUND");
            return;
        }

        transform.LookAt(target.transform);

        if (Vector3.Distance(target.transform.position, this.transform.position) < 10)
        {
            sword.SetActive(true);
            gun.SetActive(false);
            usingSword = true;
            SwordActive();

            Vector3 direction = target.transform.position - this.transform.position;
            if (direction.magnitude > 1)
            {
                anim.SetBool("isChasing", true);
                this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            }
        }

        else if (Vector3.Distance(target.transform.position, transform.position) > 10)
        {
            sword.SetActive(false);
            gun.SetActive(true);
            anim.SetBool("isChasing", false);
            usingGun = true;
            GunActive();
        }
        
        if (Input.GetKey("up"))
        {
            usingGun = true;
            usingSword = false;
        }

        if (Input.GetKey("down"))
        {
            usingGun = false;
            usingSword = true;
        }

        if (Input.GetKey("left"))
            ClearBoolens();

  
        /*
        if (usingGun)
        {
            // call a function to use the gun
            GunActive();
        }

        else if (usingSword)
        {
            // call function to use sword
            SwordActive();
        }
        */
    }
    void ClearBoolens()
    {
        bossSwordAttack.canSeePlayer = false;
        bossSwordAttack.canAttack = false;
        usingGun = false;
        usingSword = false;
        anim.SetBool("SwordStance", usingSword);
        anim.SetBool("GunStance", usingGun);
    }

    void GunActive()
    {
        // print("Gun Function is getting called");
        usingSword = false;
        bossSwordAttack.canSeePlayer = false;
        bossSwordAttack.canAttack = false;

        anim.SetBool("SwordStance", usingSword);
        anim.SetBool("GunStance", usingGun);

    }

    void SwordActive()
    {
        // print("Sword Function is getting called");
        usingGun = false;
        anim.SetBool("GunStance", usingGun);
        anim.SetBool("SwordStance", usingSword);
        //bossSwordAttack.canSeePlayer = true;
        // print("Something is wrong");
    }
}

