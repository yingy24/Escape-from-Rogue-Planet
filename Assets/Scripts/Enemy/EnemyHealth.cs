using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public GameManager gameManager;

    //Public Memeber Variables
    public float health = 20;
<<<<<<< HEAD
   // public GameObject player;
=======
    public Slider healthSlider;
    public GameObject player;
>>>>>>> f7f79c6daa84aab7959e76d231996292b57a3f15
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
<<<<<<< HEAD
=======
        healthSlider.value = health;

>>>>>>> f7f79c6daa84aab7959e76d231996292b57a3f15
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(parentTransform.gameObject);
            gameManager.player.GetComponent<CharacterMovement>().enemyTarget = null;
            gameManager.camera.GetComponent<CameraLockOn>().isLockedOn = false;
        }
    }
}

