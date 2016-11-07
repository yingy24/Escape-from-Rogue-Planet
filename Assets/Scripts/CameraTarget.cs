using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

    public GameObject camera;
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (gameManager.player.GetComponent<CharacterMovement>().enemyTarget)
            transform.LookAt(gameManager.player.GetComponent<CharacterMovement>().enemyTarget.transform);
        else
            transform.LookAt(camera.transform);
    }

}
