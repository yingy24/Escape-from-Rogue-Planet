using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Elevator : MonoBehaviour {

    //Public Classes
    public AttackCombo attackCombo;
    public Animator anim;

    //Public Member Variables
    //public bool moveUp;
    public bool requireAttack = false;

    // Use this for initialization
    void Awake () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        attackCombo = player.GetComponent<AttackCombo>();
    }
	
	// Update is called once per frame
	void Update () {
	
        //if(move)
            //transform.position = Vector3.MoveTowards(transform.position, moveHere.transform.position, 0.05f);
    }

    void OnTriggerStay(Collider other)
    {
        if (attackCombo.isAttacking)
        {
            //moveUp = true;
            anim.SetTrigger("Move Up");
            GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.1f);

        }
    }

    IEnumerator MovingRumble()
    {
        yield return new WaitForSeconds(10);
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
    }
}
