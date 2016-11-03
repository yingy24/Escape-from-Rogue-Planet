using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(gameManager.player.GetComponent<CharacterMovement>().enemyTarget.transform);

        //print(transform.localRotation);

        //print(gameManager.camera.transform.eulerAngles);
        Vector3 localAngle = this.transform.rotation.eulerAngles;
        Camera.main.transform.rotation = Quaternion.Euler(localAngle.x, localAngle.y, localAngle.z);
    }
}
