using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossAttribute : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject target, sword, gun, origin;
    public Slider healthSlider;
    public float health, energy, moveSpeed;
    public bool usingGun, usingSword, isJumping;

    private Animator anim;
    private Rigidbody rBody;
    private BossSwordAttack bossSwordAttack;
    private BossGunAttack bossGunAttack;
    private Transform parent;
    private Vector3 jumpPos;
    private float originalHealth, percentageDeduction;
    private bool isDoneJumping;
 

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        bossSwordAttack = GetComponent<BossSwordAttack>();
        bossGunAttack = GetComponent<BossGunAttack>();
        rBody = GetComponent<Rigidbody>();
        originalHealth = health;
        //isJumping = true;
        isDoneJumping = true;
        parent = transform.parent;
        percentageDeduction = 0.6f;

    }

    // Update is called once per frame
    void Update()
    {

        if (!anim) // Dummy check for an animator
        {
            print("ANIMATOR NOT FOUND");
            return;
        }

        if (health <= 0)
        {
            print("sorry i am dead");
            // something should happen now...
            return;
        }

        healthSlider.value = health;
        transform.LookAt(target.transform);

        if (health <= 0)
        {
            gameManager.cameraLockOn.SwapAndDelete(); // Function call to delete enemy from list;
            gameManager.player.GetComponent<CharacterMovement>().enemyTarget = null;
            gameManager.camera.GetComponent<CameraLockOn>().isLockedOn = false;
        }

        if (health / originalHealth <= percentageDeduction)
        {
            percentageDeduction -= 0.3f;
            anim.SetBool("isAttacking", false);
            anim.SetBool("isChasing", false);
            isJumping = true;
            anim.SetTrigger("Jump");
            print("hit");
        }

        if (isJumping)
        {
            transform.position = Vector3.MoveTowards(transform.position, origin.transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == origin.transform.position)
            {
                isJumping = false;
            }
            else
                return;
        }

        if (!isJumping)
        {
            if ((Vector3.Distance(target.transform.position, this.transform.position) < 10 || bossGunAttack.emptyAmmo) && !anim.GetBool("isAttacking"))
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
                anim.SetBool("isAttacking", false);
                anim.SetBool("isChasing", false);
                usingGun = true;
                GunActive();
            }
        }


        if (Input.GetKeyDown("up"))
        {
            health -= 10;
            //usingGun = true;
            //usingSword = false;
        }
        /* debugging
        if (Input.GetKey("down"))
        {
            usingGun = false;
            usingSword = true;
        }

        if (Input.GetKey("left"))
            ClearBoolens();
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

