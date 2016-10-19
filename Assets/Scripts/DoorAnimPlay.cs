using UnityEngine;
using System.Collections;

public class DoorAnimPlay : MonoBehaviour {

    public AttackCombo attackCombo;
    public bool requireAttack = false;

    Animator animator;
    bool doorOpen;

    // Use this for initialization
	void Start () {
        doorOpen = false;
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        attackCombo = player.GetComponent<AttackCombo>();
    }
	

	void OnTriggerEnter (Collider col) {
        if (!requireAttack)
        {
            if (col.gameObject.tag == "Player")
            {
                doorOpen = true;
                DoorTrigger("Open");
            }
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (requireAttack)
        {
            if (other.gameObject.tag == "Weapon")
            {
                if (attackCombo.isAttacking)
                {
                    doorOpen = true;
                    DoorTrigger("Open");
                }
            }
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (requireAttack)
        {
            if (doorOpen)
            {
                doorOpen = false;
                StartCoroutine(WaitToClose());
            }
        }
        else
        {
            if (doorOpen)
            {
                doorOpen = false;
                DoorTrigger("Close");
            }
        }
    }

    void DoorTrigger (string direction)
    {
        animator.SetTrigger(direction);
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(4);
        //print("Waiting to close");
        DoorTrigger("Close");
    }
}
