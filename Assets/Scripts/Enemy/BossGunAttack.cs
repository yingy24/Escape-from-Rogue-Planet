using UnityEngine;
using System.Collections;

public class BossGunAttack : MonoBehaviour {

    public GameObject bulletPrefab, spawnPoint;
    public float timeWait, ammo;
    public bool emptyAmmo;

    [HideInInspector]
    public float timePassed;


    private Animator anim;
    private BossAttribute bossAttribute;

    private float regainAmmo, maxAmmo;

    [SerializeField]
    private bool isShooting;


    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        bossAttribute = GetComponent<BossAttribute>();
        emptyAmmo = false;
        maxAmmo = ammo;
    }

    // Update is called once per frame
    void Update() {

        if (emptyAmmo)
        {
            if (regainAmmo < Time.time)
            {
                regainAmmo = Time.time + 1;
                ammo += 1;
            }
        }

        if (ammo == maxAmmo)
            emptyAmmo = false;

        if (!bossAttribute.usingGun)
            return;

        if (timePassed < Time.time && ammo > 0)
        {
            anim.SetTrigger("Shoot");
            if (isShooting)
            {
                ammo -= 1;
                timePassed = Time.time + timeWait;
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
                //bullet.transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
        else if (ammo <= 0)
        {
            emptyAmmo = true;
            bossAttribute.usingGun = false; // Turns off the gun to chase the player
            isShooting = false; // turns off gun shooting
            regainAmmo = Time.time + 1;
        }
    }

    void Shoot()
    {
        isShooting = true; // turns on gun shooting
    }
}
