﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    //Class Scripts
    public PlayerAttributes playerAttributes;
    public CameraLockOn cameraLockedOn;

    public GameObject player, bulletPrefab;
    public Transform bulletSpawnPoint;
    public Animator anim;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!playerAttributes.weaponsActive[2])
        {
            return;
        }

        if(cameraLockedOn.isLockedOn)
        {
            if (Input.GetButtonDown("Fire1") && /*  playerAttributes.weaponsObtained[0] &&*/ playerAttributes.stamina > 0.5)
            {
                player.transform.LookAt(cameraLockedOn.selectedTarget);
                anim.SetTrigger("RifleShot");
                GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;

            }
        }
    }

}