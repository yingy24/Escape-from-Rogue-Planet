using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public GameManager gameManager;

    //Public Memeber Variables
    public float health = 20;
    public Slider healthSlider;
    public GameObject player;
    public Transform parentTransform;
    public GameObject deathEffect;
    // Use this for initialization
    void Start()
    {
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        if (health <= 0)
        {
            gameManager.cameraLockOn.SwapAndDelete(); // Function call to delete enemy from list
            gameManager.playerAttributes.currentWeaponEnergy += 5;
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(parentTransform.gameObject);
            gameManager.player.GetComponent<CharacterMovement>().enemyTarget = null;
            gameManager.camera.GetComponent<CameraLockOn>().isLockedOn = false;
        }
    }
}

