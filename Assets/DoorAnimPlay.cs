using UnityEngine;
using System.Collections;

public class DoorAnimPlay : MonoBehaviour {

    public bool requireAttack = false;

    Animator animator;
    bool doorOpen;

    // Use this for initialization
	void Start () {
        doorOpen = false;
        animator = GetComponent<Animator>();
	}
	

	void OnTriggerEnter (Collider col) {

        if (requireAttack)
        {
            if (col.gameObject.tag == "Weapon")
            {
                doorOpen = true;
                DoorTrigger("Open");
            }
        }
        else
        {
            if (col.gameObject.tag == "Player")
            {
                doorOpen = true;
                DoorTrigger("Open");
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
        print("Waiting to close");
        DoorTrigger("Close");
    }
}
