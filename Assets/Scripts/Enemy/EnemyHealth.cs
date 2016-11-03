using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    public GameManager gameManager;

    //Public Memeber Variables
    public float health = 20;
   // public GameObject player;
    public Transform parentTransform;
    public GameObject effect;
    // Use this for initialization
    void Start()
    {
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(parentTransform.gameObject);
            gameManager.player.GetComponent<CharacterMovement>().enemyTarget = null;
            gameManager.camera.GetComponent<CameraLockOn>().isLockedOn = false;
        }
    }
}

