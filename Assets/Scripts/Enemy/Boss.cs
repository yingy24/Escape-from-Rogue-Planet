using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public bool usingGun, usingSword;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("up"))
            usingGun = true;

        if (Input.GetKey("down"))
            usingSword = true;

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
	
        if(usingSword)
        {
            // call function to use sword
            SwordActive();
        }

        usingGun = false;
        usingSword = false;
	}

    void ClearBoolens()
    {
        anim.SetBool("SwordStance", false);
        anim.SetBool("GunStance", false);
    }

    void GunActive()
    {
        print("Gun Function is getting called");
        anim.SetFloat("IdleMultiplyer", 100); // Have this multiplier to switch weapons quicker
        anim.SetBool("SwordStance", false);
        anim.SetBool("GunStance", true);

    }

    void SwordActive()
    {
        print("Sword Function is getting called");
        anim.SetBool("GunStance", false);
        anim.SetBool("SwordStance", true);
    }
}




