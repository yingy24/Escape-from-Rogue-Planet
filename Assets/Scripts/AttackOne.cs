using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class AttackOne : MonoBehaviour {

    public AttackCombo attackCombo;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Enemy" && attackCombo.isAttacking)
        {
            col.transform.parent.GetComponent<EnemyHealth>().health -= 5;
            GamePad.SetVibration(PlayerIndex.One, 0, 0.5f);
            StartCoroutine(StopVibrate());
        }
    }

    IEnumerator StopVibrate()
    {
        yield return new WaitForSeconds(0.15f);
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
    }
}
