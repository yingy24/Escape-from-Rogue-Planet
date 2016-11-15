using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    public CameraLockOn cameraLockedOn;

    public GameObject player, bulletPrefab;
    public Transform bulletSpawnPoint;
    public Animator anim;
    public float energyReduction;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {

        if (!playerAttributes.weaponsActive[2])
        {
            return;
        }

        if (cameraLockedOn.isLockedOn)
        {
            if (Input.GetButtonDown("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > 1)
            {
                player.transform.LookAt(cameraLockedOn.selectedTarget);
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;

            }
        }

        else
        {
            if (Input.GetButtonDown("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.stamina > 0.5)
            {
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;
               // GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;
            }
        }
    }

        public void RifleAttack()
    {
        GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;
    }
}
