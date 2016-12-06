using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    public CameraLockOn cameraLockedOn;
    public CharacterMovement cMovement;

    public GameObject player, spawnPoint, laserPrefab;
    //public Transform laser;
    public Animator anim;
    public float energyReduction, neededEnegyToShoot;


    // Use this for initialization
    void Start () {
        cMovement = GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void FixedUpdate() {

        if (!playerAttributes.weaponsActive[2])
        {
            return;
        }

        if (cameraLockedOn.isLockedOn)
        {
            if (Input.GetButtonDown("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > neededEnegyToShoot)
            {
                player.transform.LookAt(cameraLockedOn.selectedTarget);
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;

               // Fire();

            }
        }

        else
        {
            if (Input.GetButtonDown("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.currentWeaponEnergy > neededEnegyToShoot)
            {
                anim.SetTrigger("RifleShot");
                playerAttributes.currentWeaponEnergy -= energyReduction;
                playerAttributes.restTimer = 0;
                playerAttributes.regainStaminaTime = 1;

               // Fire();
            }
        }
    }
    /*
    void Fire()
    {
        RaycastHit hit;
        if(Physics.Raycast(laser.transform.position, laser.transform.forward, out hit))
        {
            print("Hit: " + hit.collider.gameObject.name);
        }
    }
    */
    public void RifleAttack()
    {
        anim.SetBool("Attacking", true);
        //laser.gameObject.SetActive(true);
        GameObject go = Instantiate(laserPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
    }

    public void laserGone()
    {
        anim.SetBool("Attacking", false);
        //laser.gameObject.SetActive(false);
    }
}
