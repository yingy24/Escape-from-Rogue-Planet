using UnityEngine;
using System.Collections;

public class BossSwordCollider : MonoBehaviour {

    public BossSwordAttack bossSwordAttack;

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player" && bossSwordAttack.isAttacking)
        {
            col.transform.GetComponent<PlayerAttributes>().health -= 5;
        }
    }


}
