using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    public CameraLockOn cameraLockedOn;

    public GameObject player, bulletPrefab;
    public Transform laser;
    public Animator anim;
    public float energyReduction, neededEnegyToShoot;


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
            if (Input.GetButton("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > neededEnegyToShoot)
            {
                player.transform.LookAt(cameraLockedOn.selectedTarget);
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;

                Fire();

            }
        }

        else
        {
            if (Input.GetButton("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > neededEnegyToShoot)
            {
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;
                // GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;


                Fire();
            }
        }
    }

    void Fire()
    {
        RaycastHit hit;
        if(Physics.Raycast(laser.transform.position, laser.transform.forward, out hit))
        {
            print("Hit: " + hit.collider.gameObject.name);
        }
    }

    public void RifleAttack()
    {
        laser.gameObject.SetActive(true);
        //GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;
    }

    public void laserGone()
    {
        laser.gameObject.SetActive(false);
    }
}
