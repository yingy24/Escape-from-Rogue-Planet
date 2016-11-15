using UnityEngine;
using System.Collections;

public class DoorAnimPlay : MonoBehaviour {

    //public AttackCombo attackCombo;
    //public bool requireButtonPress = false;

    Animator animator;
    bool doorOpen;

    // Use this for initialization
	void Start () {
        doorOpen = false;
        animator = GetComponent<Animator>();
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //attackCombo = player.GetComponent<AttackCombo>();
    }
	
    
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            doorOpen = true;
            DoorTrigger("Open");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorOpen = true;
            DoorTrigger("Open");
        }
    }

    void OnTriggerExit (Collider col)
    {
        doorOpen = false;
        DoorTrigger("Close");
    }

    void DoorTrigger (string direction)
    {
        animator.SetTrigger(direction);
    }

    
}
