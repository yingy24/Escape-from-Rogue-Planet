using UnityEngine;
using System.Collections;

public class BossGunAttack : MonoBehaviour {
    
    private Animator anim;
    private BossAttribute bossAttribute;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        bossAttribute = GetComponent<BossAttribute>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!bossAttribute.usingGun)
            return;
	}
}
