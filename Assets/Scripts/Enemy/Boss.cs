using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {


    public float attackRate;
    public bool usingGun, usingSword;


    private Animator anim;

    [SerializeField]
    private string[] comboParam;
    [SerializeField]
    private float timePassed;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        timePassed = 0;

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
	void Update () {

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

        if (!anim)
        {
            print("ANIMATOR NOT FOUND");
        }

        if(usingGun)
        {
            // call a function to use the gun
            GunActive();
        }
	
        else if(usingSword)
        {
            // call function to use sword
            SwordActive();
        }

        if (timePassed < Time.time)
        {
            //print(Time.time);
            timePassed = Time.time + attackRate;
            //print(timePassed);
            //print("Something is wrong");
        }
    }

    void ClearBoolens()
    {
        usingGun = false;
        usingSword = false;
        anim.SetBool("SwordStance", usingSword);
        anim.SetBool("GunStance", usingGun);
    }

    void GunActive()
    {
        // print("Gun Function is getting called");
        usingSword = false;
        anim.SetBool("SwordStance", usingSword);
        anim.SetBool("GunStance", usingGun);

    }

    void SwordActive()
    {
        // print("Sword Function is getting called");
        usingGun = false;
        anim.SetBool("GunStance", usingGun);
        anim.SetBool("SwordStance", usingSword);

        int randomNum = Random.Range(0, 3);
        /*
        if (timePassed <Time.time)
        {
            print(Time.time);
            timePassed = Time.time + attackRate;
            print("Gettting called");
            anim.SetTrigger(comboParam[randomNum]);
        }
        */
    }
}




