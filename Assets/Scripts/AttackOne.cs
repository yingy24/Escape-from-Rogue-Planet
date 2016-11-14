using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class AttackOne : MonoBehaviour {

    public CharacterMovement characterMovement;
    public PlayerAttributes playerAttributes;
    public float damageDealt;
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerEnter(Collider col)
    {
        /*
        if (col.transform.tag == "Enemy" && attackCombo.isAttacking)
        {
            col.transform.parent.GetComponent<EnemyHealth>().health -= 5;
            GamePad.SetVibration(PlayerIndex.One, 0, 0.5f);
            StartCoroutine(StopVibrate());
        }
        */
        if (col.transform.tag == "Enemy" && characterMovement.isAttacking)
        {

            col.transform.GetComponent<EnemyHealth>().health -= damageDealt;
            GamePad.SetVibration(PlayerIndex.One, 0, 0.75f);
            StartCoroutine(StopVibrate());
        }
    }

    IEnumerator StopVibrate()
    {
        yield return new WaitForSeconds(0.15f);
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
    }
}
